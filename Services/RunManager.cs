using SlayTheStats.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SlayTheStats.Services;

public class RunManager {
    private string RunFolder { get; set; }
    private List<string> CharactersToLoad { get; set; }

    public List<Run> Runs { get; set; } = new();

    public RunManager(string runFolder, List<string> characters){
        RunFolder = runFolder;
        CharactersToLoad = characters;
    }

    public void LoadRuns(){
        var di = new DirectoryInfo(RunFolder);
        foreach(var character in CharactersToLoad){
            foreach(var characterFolder in di.EnumerateDirectories($"{character}*"))
            {                
                foreach(var run in characterFolder.EnumerateFiles("*.run")){
                    LoadRun(run);
                }        
            }
        }
    }

    public void LoadRun(FileInfo runFile){
        string runAsText = "";
        
        using(var stream = runFile.OpenText()){
            runAsText = stream.ReadToEnd();
        }
        
        var run = JsonSerializer.Deserialize<Run>(runAsText);
        run.Character = runFile.Directory.Name;
        Runs.Add(run);
    }

    public List<string> LoadCharacterList(){
        var di = new DirectoryInfo(RunFolder);
        var dirs = new List<string>();
        foreach(var characterToLoad in CharactersToLoad)
        {
            foreach(var dir in di.EnumerateDirectories($"{characterToLoad}*")){
                dirs.Add(dir.Name);
            }
        }
        return dirs;
    }
}