using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine("build");
    }

    public bool createTower(Tower tower, Vector3 spawnPos)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (!bank)
            return false;

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, spawnPos, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
                
        }

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }

        }
    }

   
}
