using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")] 
    [SerializeField] private float moveDistance = 1;
    [SerializeField] private float moveSpeed = 5;
    public float GetMoveDistance => moveDistance;
    public float GetMoveSpeed => moveSpeed;

    [Header("Level Settings")]

    [Header("UI")]
    [SerializeField] private Text actionPointText;

    enum GameState
    {
        Start,
        InGame,
        End
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void NextAction()
    {
        //Appeler les fonctions qui doivent se faire à chaque action
    }

}
