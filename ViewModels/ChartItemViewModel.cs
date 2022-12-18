namespace SlayTheStats.ViewModels;

public class ChartItemViewModel : ViewModelBase
{
    public string Label { get; set; }
    public float Count { get; set; }

    public ChartItemViewModel(string label, float count){
        Label = label;
        Count = count;
    }
}