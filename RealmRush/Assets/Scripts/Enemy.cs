using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Bank bank;

    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();

    }

    public void rewardGold()
    {
        if (!bank)
            return;

        bank.Deposit(goldReward);
    }

    public void stealGold()
    {
        if (!bank)
            return;

        bank.Withdraw(goldPenalty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
