using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{

    [ContextMenu("Start Sherlock Game")]
    void StartSherlockGame()
    {
        var sherlockGame = gameObject.GetComponent<SherlockGame>();
        sherlockGame.Setup();
    }

    [ContextMenu("Start Blood Game")]
    void StartBloodGame()
    {
        var bloodGame = gameObject.GetComponent<BloodGame>();
        bloodGame.Setup();
    }

    [ContextMenu("Start Heart Game")]
    void StartHeartGame()
    {
        var heartGame = gameObject.GetComponent<HeartGame>();
        heartGame.Setup();
    }
}
