using System.Collections.Generic;
using System.Linq;
using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public class RelicsFilterViewModel : BaseFilterViewModel
{
    public List<string> Relics { get; set; } = new();
    public List<string> FilteredRelics { get; set; } = new();
    public override string FilterName => "RelicFilter";
    public override IEnumerable<Run> ApplyFilter(IEnumerable<Run> runsToFilter)
    {
        return runsToFilter.Where(x => FilteredRelics.All(relic => x.Relics.Contains(relic)));
    }

    public override void LoadRunData(IEnumerable<Run> runs){
        HashSet<string> uniqueRelics = new();
        foreach(var run in runs){
            foreach(var relic in run.Relics){
                uniqueRelics.Add(relic);
            }
        }

        Relics = uniqueRelics.ToList();
        Relics.Sort();
    }
}