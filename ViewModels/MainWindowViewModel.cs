using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SlayTheStats.Services;
using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _greeting = "Welcome!";
    public string Greeting { get { return _greeting; } set {_greeting = value; }}
    private RunManager Manager;
    private SettingsManager Settings = new();
    public ObservableCollection<RunViewModel> Runs { get; } = new();
    public StatsViewModel Stats { get; set; } 
    public ObservableCollection<BaseFilterViewModel> Filters { get; set; } = new();
    public ObservableCollection<BaseFilterViewModel> AvailableFilters { get; set; } = new();
    public int SelectedFilter { get; set; }
    public BaseFilterViewModel FilterToAdd { get; set; }


    public MainWindowViewModel(){
        Settings.LoadSettings();
        Manager = new RunManager(Settings.CurrentSettings.RunFolder);
        RxApp.MainThreadScheduler.Schedule(LoadCharacterList);
        RxApp.MainThreadScheduler.Schedule(LoadRuns);
        RxApp.MainThreadScheduler.Schedule(LoadAvailableFilters);
    }

    private async void LoadRuns() {
        await Task.Run(() => Manager.LoadRuns());
        foreach(var run in Manager.Runs){
            Runs.Add(new RunViewModel(run));
        }
        Stats = new StatsViewModel(Runs);
    }

    private async void LoadCharacterList() {
        var characterFilter = new CharacterFilterViewModel();
        foreach(var character in Manager.LoadCharacterList()){
            characterFilter.Characters.Add(character);
            characterFilter.FilteredCharacters.Add(character);
        }

        Filters.Add(characterFilter);
    }

    private async void LoadAvailableFilters() {
        AvailableFilters.Add(new CharacterFilterViewModel());
        AvailableFilters.Add(new VictoryFilterViewModel());
        AvailableFilters.Add(new RelicsFilterViewModel());
    }

    public void ApplyFilter(){
        Runs.Clear();
        IEnumerable<Run> runsToFilter = Manager.Runs;
        foreach(var filter in Filters){
            //Console.WriteLine($"Before applying filter {filter.FilterName}: {runsToFilter.Count()}");
            runsToFilter = filter.ApplyFilter(runsToFilter);
            //Console.WriteLine($"After applying filter {filter.FilterName}: {runsToFilter.Count()}");
        }
        
        foreach(var run in runsToFilter){
            Runs.Add(new RunViewModel(run));
        }
    }

    public void AddFilter() {
        FilterToAdd.LoadRunData(Manager.Runs);
        Filters.Add(FilterToAdd);
    }

    public void DeleteFilter() {
        Filters.RemoveAt(SelectedFilter);
    }
}
