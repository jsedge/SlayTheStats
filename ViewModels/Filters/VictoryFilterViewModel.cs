using SlayTheStats.Models;

using System.Collections.Generic;
using System.Linq;

namespace SlayTheStats.ViewModels;

public class VictoryFilterViewModel : BaseFilterViewModel
{
    public override string FilterName => "VictoryFilter";

    public int SelectedIndex { get; set; } = 1;

    private bool Filter => SelectedIndex == 1;

    public override IEnumerable<Run> ApplyFilter(IEnumerable<Run> runToFilter){
        return runToFilter.Where(x => x.Victory == Filter);
    }

    public override void LoadRunData(IEnumerable<Run> runs){
        return;
    }
}