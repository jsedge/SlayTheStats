using System;

namespace SlayTheStats.Models;

public class Settings {
    public string RunFolder { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Steam/steamapps/common/SlayTheSpire/runs";
}