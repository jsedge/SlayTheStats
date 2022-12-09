using System;
using System.IO;
using System.Text.Json;

using SlayTheStats.Models;

namespace SlayTheStats.Services;

public class SettingsManager {
    public Settings CurrentSettings { get; set; }

    public void LoadSettings(){
        var settingsFile = new FileInfo($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}/slaythestats");
        string settingsAsText = "";
        if(settingsFile.Exists){
            using(var stream = settingsFile.OpenText()){
                settingsAsText = stream.ReadToEnd();
            }

            CurrentSettings = JsonSerializer.Deserialize<Settings>(settingsAsText);
        }
        else
        {
            using var stream = File.Create(settingsFile.FullName);
            CurrentSettings = new();
            JsonSerializer.Serialize(stream, CurrentSettings);
        }
    }
}