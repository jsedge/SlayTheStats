using System;
using System.Collections.Generic;

using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public class RunViewModel : ViewModelBase {
    private readonly Run _run;

    public RunViewModel(Run run){
        _run = run;
    }

    public string Character => _run.Character;
    public bool Victory => _run.Victory;
    public string Ascension => _run.Ascension.ToString();
    public int Playtime => _run.Playtime;
    public int Score => _run.Score;
    public int FloorReached => _run.FloorReached;
    public int Smiths => _run.Smiths;
    public int Rests => _run.Rests;
    public int DeckSize => _run.Deck.Count;
    public string NeowBonus => _run.NeowBonus;
    public string? Killer => _run.Killer;
    public List<string> CardRemovals => _run.CardRemovals;
    public List<string> Deck => _run.Deck;
    public List<string> Relics => _run.Relics;

    public string Summary => $"{Character} A{Ascension} {(_run.Victory ? "Win" : "Loss")}";
}