using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTutorial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Social.ReportProgress("CgkIwt3h49cJEAIQAQ", 100.0f, success => {
                // handle success or failure
            });
        }
    }
}
