using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using Context;

public class LoseFight : MonoBehaviour
{
    public Text TextLostCoins;
    public Text TextCurrentCoins;
    public static SQLdb DBManager = new SQLdb();
    public Hero hero;

    public int Coins;
    public int CurrentCoins;

    void Start()
    {
        hero = DBManager.GetHero("moshe");
        Coins = new System.Random().Next(1, 4) * 10;
        CurrentCoins = hero.Inventory.Coins;
        Debug.Log(hero.Inventory.Coins);    
        CurrentCoins -= Coins;
        UpdateUI();
    }

    public void UpdateUI()
    {
        TextLostCoins.text = Coins.ToString();
        TextCurrentCoins.text = CurrentCoins.ToString();
        hero.Inventory.Coins = CurrentCoins;
        DBManager.SaveHero(hero);

    }
}
