using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCoinHeal : MonoBehaviour
{
    public int removeCoin;

    private void Start()
    {
        GameManager.Instance.PeopleToHeal++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if(removeCoin  > GameManager.Instance.totalPillule)
            {
                
                Debug.Log("TA LOOSEEE");
            }
            else
            {
                GameManager.Instance.RemovePillule(removeCoin);
                Debug.Log("TA WINNNNNN");

            }
           
            
        }
    }
}
