using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    public static EnemieManager Instance;

    public GameObject[] cacEnemies;
    public GameObject[] rangeEnemies;
    public int enemieFinish;

    private int enemyNumber;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        foreach (GameObject cacEnemy in cacEnemies)
        {
            if (cacEnemy != null)
                enemyNumber++;
        }

        foreach (GameObject rangeEnemy in rangeEnemies)
        {
            if (rangeEnemy != null)
                enemyNumber++;
        }
    }

    private void Update()
    {
        if (enemieFinish == enemyNumber && enemyNumber != 0)
        {
            GameManager.Instance.EnemyEndMovement();
            enemieFinish = 0;
        }
    }

    public void Action()
    {
        for (int i = 0; i < cacEnemies.Length; i++)
        {
            if(cacEnemies[i] != null)
                cacEnemies[i].GetComponent<EnemieCac>().Action();
        }

        for (int i = 0; i < rangeEnemies.Length; i++)
        {
            if(rangeEnemies[i] != null)
                rangeEnemies[i].GetComponent<EnemieRange>().Action();
        }

        
    }
}
