using SlayTheStats.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SlayTheStats.Services;

public class RunManager {
    private string RunFolder { get; set; }

    public List<Run> Runs { get; set; } = new();

    public RunManager(string runFolder){
        RunFolder = runFolder;
    }

    public void LoadRuns(){
        var di = new DirectoryInfo(RunFolder);
        foreach(var character in di.EnumerateDirectories()){
            foreach(var run in character.EnumerateFiles("*.run")){
                LoadRun(run);
            }        
        }
    }

    public void LoadRun(FileInfo runFile){
        string runAsText = "";
        
        using(var stream = runFile.OpenText()){
            runAsText = stream.ReadToEnd();
        }
        
        var run = JsonSerializer.Deserialize<Run>(runAsText);
        Runs.Add(run);
    }

    public List<string> LoadCharacterList(){
        var di = new DirectoryInfo(RunFolder);
        var dirs = new List<string>();
        foreach(var dir in di.EnumerateDirectories()){
            dirs.Add(dir.Name);
        }
        return dirs;
    }
}