using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemieManager : MonoBehaviour
{
    public GameObject[] cacEnemies;
    public GameObject[] rangeEnemies;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
