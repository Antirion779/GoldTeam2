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
    private int nbrPillule = 0;
    public int totalPillule => nbrPillule;
    private int actionPoint = 0;

    [Header("UI")]
    [SerializeField] private Text pilluleText;

    enum GameState
    {
        Start,
        InGame,
        Paused,
        MiniGame,
        End
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        pilluleText.text = nbrPillule.ToString();
    }

    public void AddPillule(int add)
    {
        nbrPillule += add;
        pilluleText.text = nbrPillule.ToString();
    }

    public void RemovePillule(int remove)
    {
        nbrPillule -= remove;
        pilluleText.text = nbrPillule.ToString();
    }

    public void NextAction()
    {
        actionPoint++;
        //Appeler les fonctions qui doivent se faire � chaque action
    }

}
