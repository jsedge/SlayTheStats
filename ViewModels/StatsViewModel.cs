using System;
using System.Collections.ObjectModel;
using System.Linq;
using ReactiveUI;

namespace SlayTheStats.ViewModels;

public class StatsViewModel : ViewModelBase {
    public ObservableCollection<RunViewModel> Runs { get; set; }

    public StatsViewModel(ObservableCollection<RunViewModel> runs){
        Runs = runs;

        this.WhenAnyValue(x => x.Runs.Count)
            .Subscribe(x => this.RefreshStats());
    }

    private void RefreshStats()
    {
        // Use reflection to flag all of our stats as changed when the runs filtered changes to force UI updates
        foreach(var stat in this.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)){
            this.RaisePropertyChanged(stat.Name);
        }
    }

    private float Wins => Runs.Where(x => x.Victory).Count();
    private float WinRate => (Wins / Runs.Count) * 100;
    private double AveragePlaytime => Runs.Average(x => x.Playtime) / 60;
    private double MaxPlaytime => Runs.Max(x => x.Playtime) / 60;
    private double MinPlaytime => Runs.Min(x => x.Playtime) / 60;
    private string MostFrequentNeowBonus => Runs.GroupBy(x => x.NeowBonus).OrderByDescending(x => x.Count()).First().Key;
     private double AvgScore => Runs.Average(x => x.Score);
    private double MaxScore => Runs.Max(x => x.Score);
    private double MinScore => Runs.Min(x => x.Score);
    private double AvgFloor => Runs.Average(x => x.FloorReached);
    private double AvgSmith => Runs.Average(x => x.Smiths);
    private double MaxSmith => Runs.Max(x => x.Smiths);
    private double MinSmith => Runs.Min(x => x.Smiths);
    private double AvgRests => Runs.Average(x => x.Rests);
    private double MaxRests => Runs.Max(x => x.Rests);
    private double MinRests => Runs.Min(x => x.Rests);
    private double AvgDeckSize => Runs.Average(x => x.DeckSize);
    private double MaxDeckSize => Runs.Max(x => x.DeckSize);
    private double MinDeckSize => Runs.Min(x => x.DeckSize);

}