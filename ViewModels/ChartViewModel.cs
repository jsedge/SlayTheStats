using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using SlayTheStats.Models;

namespace SlayTheStats.ViewModels;

public class ChartViewModel 
{
    struct WinPickCount 
    {
        public WinPickCount(int picks, int wins)
        {
            Picks = picks;
            Wins = wins;
        }
        public int Picks { get; set; }
        public int Wins { get; set; }
    }
    public delegate void ChartDelegate();
    public ObservableCollection<RunViewModel> Runs { get; set; }
    public ObservableCollection<ChartItemViewModel> Items { get; set; } = new();

    public List<string> Charts { get; set; }
    public string SelectedChart { get; set; }

    public ChartViewModel(ObservableCollection<RunViewModel> runs)
    {
        Runs = runs;
        Charts = new List<string>(){
            "Boss Kill Count",
            "Top Win Rate cards",
            "Worst Win Rate cards",
            "Most Removed Cards",
            "Top Win Rate relics",
            "Worst Win Rate relics"
        };
    }

    public void ShowChart()
    {
        Items.Clear();
        // TODO: Clean this up, I don't like the mapping magic strings back to functions
        switch(SelectedChart)
        {
            case "Boss Kill Count":
                GetBossKillCount();
                break;
            case "Top Win Rate cards":
                GetTopCards();
                break;
            case "Worst Win Rate cards":
                GetBottomCards();
                break;
            case "Most Removed Cards":
                GetMostRemovedCards();
                break;
            case "Top Win Rate relics":
                GetTopRelics();
                break;
            case "Worst Win Rate relics":
                GetBottomRelics();
                break;
        }
    }   

    public void GetBossKillCount()
    {
        foreach(var run in Runs.Where(x => !x.Victory && !string.IsNullOrEmpty(x.Killer))
                               .GroupBy(x => x.Killer)
                               .OrderByDescending(x => x.Count())
                               .Take(10)){
            Items.Add(new ChartItemViewModel(run.Key, run.Count()));
        }        
    }

    public void GetTopCards()
    {
        var topCards = GetCardsByWinrate();
        foreach(var card in topCards.OrderByDescending(x => x.Value).Take(25)){
            Items.Add(new ChartItemViewModel(card.Key, card.Value));
        }
    }

    public void GetBottomCards()
    {
        var cards = GetCardsByWinrate();
        foreach(var card in cards.OrderBy(x => x.Value).Take(25)){
            Items.Add(new ChartItemViewModel(card.Key, card.Value));
        }
    }

    public Dictionary<string, float> GetCardsByWinrate()
    {
        Dictionary<string, WinPickCount> winsPerCard = new();
        foreach(var run in Runs)
        {
            foreach(var card in run.Deck)
            {
                if(winsPerCard.ContainsKey(card))
                {
                    var cardWins = winsPerCard[card];
                    cardWins.Picks += 1;
                    cardWins.Wins += (run.Victory ? 1 : 0);
                    winsPerCard[card] = cardWins;
                }
                else
                {
                    winsPerCard[card] = new(1, (run.Victory ? 1 : 0));
                }

            }
        }

        Dictionary<string, float> cardByWinrate = new();
        foreach(var card in winsPerCard)
        {
            cardByWinrate[card.Key] = ((float)card.Value.Wins) / ((float)card.Value.Picks) * 100; 
        }
        return cardByWinrate;
    }

    public void GetMostRemovedCards()
    {
        Dictionary<string, int> removeCounts = new();
        foreach(var run in Runs)
        {
            foreach(var removal in run.CardRemovals)
            {
                if(removeCounts.ContainsKey(removal))
                    removeCounts[removal] += 1;
                else
                    removeCounts[removal] = 1;
            }
        }

        foreach(var removal in removeCounts.OrderByDescending(x => x.Value).Take(10))
        {
            Items.Add(new ChartItemViewModel(removal.Key, removal.Value));
        }
    }

    public Dictionary<string, float> GetRelicsByWinrate()
    {
        Dictionary<string, WinPickCount> winsPerRelic = new();
        foreach(var run in Runs)
        {
            foreach(var relic in run.Relics)
            {
                if(winsPerRelic.ContainsKey(relic))
                {
                    var relicWins = winsPerRelic[relic];
                    relicWins.Picks += 1;
                    relicWins.Wins += (run.Victory ? 1 : 0);
                    winsPerRelic[relic] = relicWins;
                }
                else
                {
                    winsPerRelic[relic] = new(1, (run.Victory ? 1 : 0));
                }

            }
        }

        Dictionary<string, float> relicByWinrate = new();
        foreach(var relic in winsPerRelic)
        {
            relicByWinrate[relic.Key] = ((float)relic.Value.Wins) / ((float)relic.Value.Picks) * 100; 
        }
        return relicByWinrate;
    }

    public void GetTopRelics()
    {
        var relics = GetRelicsByWinrate();
        foreach(var relic in relics.OrderByDescending(x => x.Value).Take(25)){
            Items.Add(new ChartItemViewModel(relic.Key, relic.Value));
        }
    }

    public void GetBottomRelics()
    {
        var relics = GetRelicsByWinrate();
        foreach(var relic in relics.OrderBy(x => x.Value).Take(25)){
            Items.Add(new ChartItemViewModel(relic.Key, relic.Value));
        }
    }
}