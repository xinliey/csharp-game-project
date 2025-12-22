using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] PlayerScoreRecord player;
    private void Start()
    {
        amount = player.money;
        UpdateText();
    }
    private void UpdateText()
    {
        player.money = amount;
        text.text = amount.ToString();
    }
    public void Add(int moneyGain)
    {
        amount += moneyGain;
        UpdateText();
    }
    //check if we have enough money to buy item
    internal bool Check(int totalPrice)
    {
        return amount >= totalPrice;
    }
    internal void Decrease(int totalPrice)
    {
        amount -= totalPrice;
        if (amount < 0) { amount = 0; }
        UpdateText();
    }
}
