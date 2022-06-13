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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (enemieFinish == (cacEnemies.Length + rangeEnemies.Length) && (cacEnemies.Length + rangeEnemies.Length) != 0)
        {
            GameManager.Instance.EnemyEndMovement();
            enemieFinish = 0;
        }
    }

    public void Action()
    {
        for (int i = 0; i < cacEnemies.Length; i++)
        {
            cacEnemies[i].GetComponent<EnemieCac>().Action();
        }

        for (int i = 0; i < rangeEnemies.Length; i++)
        {
            rangeEnemies[i].GetComponent<EnemieRange>().Action();
        }

        
    }
}
