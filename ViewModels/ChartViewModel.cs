using System.Collections.ObjectModel;

namespace SlayTheStats.ViewModels;

public class ChartViewModel 
{
    public ObservableCollection<RunViewModel> Runs { get; set; }

    public ChartViewModel(ObservableCollection<RunViewModel> runs)
    {
        Runs = runs;
    }
}