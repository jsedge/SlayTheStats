using System.Collections.Generic;

using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public abstract class BaseFilterViewModel : ViewModelBase
{
    public virtual string FilterName => "BaseFilter";
    public abstract IEnumerable<Run> ApplyFilter(IEnumerable<Run> runsToFilter);

    public abstract void LoadRunData(IEnumerable<Run> runsToLoadFrom);
}