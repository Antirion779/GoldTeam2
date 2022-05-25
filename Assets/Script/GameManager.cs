using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private float moveDistance = 1, moveSpeed = 5;
    public float GetMoveDistance => moveDistance;
    public float GetMoveSpeed => moveSpeed;

    private int actionPoint;
    public int GetActionPoint => actionPoint;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void NextAction()
    {
        actionPoint--;
        //Appeler les fonctions qui doivent se faire à chaque action
    }

}
