using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickCoin : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.AddPills(1);
            Debug.Log("ta eu les sousou)");
            Destroy(gameObject);
        }
    }
}
