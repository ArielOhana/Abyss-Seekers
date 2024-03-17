using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Context;

public class LoseFight : MonoBehaviour
{
    [SerializeField] public Text TextLostCoins;
    [SerializeField] public Text TextCurrentCoins;
    public static SQLdb DBManager = new SQLdb();

    public int Coins;
    public int CurrentCoins;

    void Start()
    {
        LoseActive();
    }
    public void LoseActive()
    {
        if (MainMenu.currentHero != null) {
            Coins = new System.Random().Next(1, 4) * 10;
            CurrentCoins = MainMenu.currentHero.Inventory.Coins;
            Debug.Log(MainMenu.currentHero.Inventory.Coins);
            CurrentCoins -= Coins;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        TextLostCoins.text = Coins.ToString();
        TextCurrentCoins.text = CurrentCoins.ToString();
        MainMenu.currentHero.Inventory.Coins = CurrentCoins;
        DBManager.SaveHero(MainMenu.currentHero);

    }
}
