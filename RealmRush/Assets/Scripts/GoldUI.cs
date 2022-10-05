using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GoldUI : MonoBehaviour
{

    TextMeshProUGUI UILabel;
    Bank bank;

    private void Start()
    {
        UILabel = GetComponent<TextMeshProUGUI>();
        bank = FindObjectOfType<Bank>();

        UILabel.text = "Gold: " + bank.CurrentBalance;
    }

    public void updateUI(int currentBalance)
    {
        if(UILabel)
            UILabel.text = "Gold: " + currentBalance;
    }
}
