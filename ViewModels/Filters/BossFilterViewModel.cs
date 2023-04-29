using SlayTheStats.Models;

using System.Collections.Generic;
using System.Linq;
using System;

namespace SlayTheStats.ViewModels;

public class BossFilterViewModel : BaseFilterViewModel
{
    public override string FilterName => "BossFilter";

    public string SelectedItem { get; set; }

    public List<string> Bosses { get; } = new() {
        "Hexaghost",
      "The Guardian",
      "Slime Boss",
      "Collector",
      "Nemesis",
      "Automaton",
      "Time Eater",
      "Awakened One",
      "Donu and Deca",
      "The Heart",

    };

    public override IEnumerable<Run> ApplyFilter(IEnumerable<Run> runToFilter){
        Console.WriteLine(SelectedItem);
        return runToFilter.Where(x => x.Fights.Any(y => y.Enemy.Equals(SelectedItem)));
    }

    public override void LoadRunData(IEnumerable<Run> runs){
        return;
    }
}