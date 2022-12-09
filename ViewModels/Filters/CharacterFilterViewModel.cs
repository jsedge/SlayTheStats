using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public class CharacterFilterViewModel : BaseFilterViewModel {

    public override string FilterName => "CharacterFilter";
    private string _character;
    public ObservableCollection<string> Characters { get; } = new();
    public ObservableCollection<string> FilteredCharacters { get; set; } = new();
   

    public string Name => Prettify(_character);
    public string Character => _character;

    private string Prettify(string stringToPrettify){
        var ti = new CultureInfo("en-US", false).TextInfo;
        var pretty = ti.ToTitleCase(stringToPrettify.Replace("_", " ").ToLower());
        Console.WriteLine($"Prettify returned {pretty}");
        return pretty;
    }

    public override IEnumerable<Run> ApplyFilter(IEnumerable<Run> runsToFilter){
        return runsToFilter.Where(x => FilteredCharacters.Contains(x.Character));
    }

    public override void LoadRunData(IEnumerable<Run> runs){
        return;
    }
}