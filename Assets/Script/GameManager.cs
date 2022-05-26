using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player")] 
    [SerializeField] private float moveSpeed = 5;
    private float moveDistance = 1;
    public float GetMoveSpeed => moveSpeed;
    public float GetMoveDistance => moveDistance;

    [Header("Level Settings")]
    [SerializeField] private List<EventTime> listEvent = new List<EventTime>();
    private int nbrPills = 0;
    public int TotalPills => nbrPills;
    private int actionPoint = 0;
    private int peopleToHeal = 0;
    public int PeopleToHeal { get => peopleToHeal; set => peopleToHeal = value; }
    [SerializeField] private GameObject openDoor;

    [Header("UI")]
    [SerializeField] private Text pillsText;

    [Header("GameSystem")] 
    private Grid ldGrid;
    private GameState actualGameState = GameState.Start;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }

    public enum GameState
    {
        Start,
        InGame,
        Paused,
        MiniGame,
        End
    }

    [Serializable]
    struct EventTime
    {
        public int actionTime;
        public GameObject actionToDo;
    }


    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        pillsText.text = nbrPills.ToString();

        ldGrid = FindObjectOfType<Grid>();
        moveDistance = ldGrid.cellSize.x;

        foreach (EventTime fixedEvent in listEvent)
        {
            fixedEvent.actionToDo.SetActive(false);
        }
    }

    public void AddPills(int add)
    {
        nbrPills += add;
        pillsText.text = nbrPills.ToString();
    }

    public void RemovePills(int remove)
    {
        nbrPills -= remove;
        pillsText.text = nbrPills.ToString();
        PeopleHeal();
    }

    public void NextAction()
    {
        actionPoint++;
        //Appeler les fonctions qui doivent se faire � chaque action
        ActivateEvent();
    }

    private void ActivateEvent()
    {
        foreach (EventTime fixedEvent in listEvent)
        {
            if (fixedEvent.actionTime == actionPoint)
            {
                fixedEvent.actionToDo.SetActive(true);
            }
        }
    }

    public void PeopleHeal()
    {
        peopleToHeal--;
        if (peopleToHeal == 0)
        {
            openDoor.SetActive(true);
        }
    }
}
