using System;
using System.Collections.Generic;

namespace SlayTheStats.Models;

public class Settings {
    public string RunFolder { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Steam/steamapps/common/SlayTheSpire/runs";
    public List<string> CharactersToLoad { get; set; } = new() {"WATCHER", "THE_SILENT", "IRONCLAD", "DEFECT"};
}