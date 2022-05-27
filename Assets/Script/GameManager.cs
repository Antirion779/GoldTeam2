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
    private List<Event> listEventEnable = new List<Event>();
    [SerializeField] private List<EnemieBase> enemyList = new List<EnemieBase>();
    private int nbrPills = 0;
    public int TotalPills => nbrPills;
    private int actionPoint = 0;
    public int ActionPoint => actionPoint;
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

        for (int i = 0; i < listEvent.Count;)
        {
            if (listEvent[i].actionToDo != null)
            {
                listEvent[i].actionToDo.SetActive(false);
                i++;
            }
            else
                listEvent.Remove(listEvent[i]);
        }

        for (int i = 0; i < enemyList.Count;)
        {
            if (enemyList[i] == null)
                enemyList.Remove(enemyList[i]);
            else
                i++;
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

        foreach (EnemieBase enemy in enemyList)
        {
            enemy.Action();
        }

        foreach (Event eventEnable in listEventEnable)
        {
            eventEnable.ActionLaunch();
        }


        ActivateEvent();
    }

    private void ActivateEvent()
    {
        foreach (EventTime fixedEvent in listEvent)
        {
            if (fixedEvent.actionTime == actionPoint)
            {
                fixedEvent.actionToDo.SetActive(true);
                listEventEnable.Add(fixedEvent.actionToDo.GetComponent<Event>());
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
