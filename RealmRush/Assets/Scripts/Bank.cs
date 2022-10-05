using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;

    public int CurrentBalance { get { return currentBalance; } }

    GoldUI UI;

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);

        UI.updateUI(currentBalance);
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);

        UI.updateUI(currentBalance);

        if (currentBalance < 0)
            reloadScene();

    }

    void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }


    // Start is called before the first frame update
    void Start()
    {
        currentBalance = startingBalance;

        UI = FindObjectOfType<GoldUI>();

        if (UI)
            UI.updateUI(currentBalance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
