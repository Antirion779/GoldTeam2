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
}
