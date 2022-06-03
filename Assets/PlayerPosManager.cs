using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosManager
{
    public static PlayerPosManager Instance { get; private set; }

    public Vector3 CurrentPlayerStartPos;
    public List<Vector3> ListCurrentPlayerPos = new List<Vector3>();

    public Vector3 PreviousPlayerStartPos;
    public List<Vector3> ListPreviousPlayerPos = new List<Vector3>();

    public static void Init()
    {

        if(Instance == null)
        {
            Instance = new PlayerPosManager();
        }
    }


}
