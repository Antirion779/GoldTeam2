using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    public static EnemieManager Instance;

    public GameObject[] cacEnemies;
    public GameObject[] rangeEnemies;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Action()
    {
        for (int i = 0; i < cacEnemies.Length; i++)
        {
            cacEnemies[i].GetComponent<EnemieCac>().Action();
        }

        for (int i = 0; i < rangeEnemies.Length; i++)
        {
            cacEnemies[i].GetComponent<EnemieRange>().Action();
        }
    }
}
