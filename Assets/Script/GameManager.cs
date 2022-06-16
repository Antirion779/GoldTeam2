using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.SceneManagement;
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
    private GameObject player;
    public GameObject Player { get => player; set => player = value; }
    private Animator playerAnim;
    public Animator PlayerAnim { get => playerAnim; set => playerAnim = value; }

    [Header("Level Settings")]
    [SerializeField] private List<EventTime> listEvent = new List<EventTime>();
    private List<Event> listEventEnable = new List<Event>();
    public List<Door> listDoor = new List<Door>();
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private List<GameObject> listPeopleToHeal = new List<GameObject>();
    private int nbrPills = 0;
    public int TotalPills => nbrPills;
    private int actionPoint = 0;
    public int ActionPoint { get => actionPoint; set => actionPoint = value; }
    private int peopleToHeal = 0;
    public int PeopleToHeal { get => peopleToHeal; set => peopleToHeal = value; }
    [SerializeField] private GameObject openDoor;

    private List<GameObject> oilCaseList = new List<GameObject>();
    public List<GameObject> OilCaseList { get => oilCaseList; set => oilCaseList = value; }

    [Header("UI")]
    [SerializeField] private Text pillsText;
    [SerializeField] private Text actionText;
    [SerializeField] private Text actionText2;

    [Header("Previous Game Effect")]
    [SerializeField] private PreviousGameEffect previousGameEffect;

    [Header("GameSystem")]
    public GameObject winMenuUI;
    public GameObject[] etoiles;
    private Grid ldGrid;
    private GameState actualGameState = GameState.Start;
    public GameState ActualGameState { get => actualGameState; set => actualGameState = value; }
    
    public List<int> scoreLevels => new List<int>();

    public SliderMovement sliderMovement;
    public enum GameState
    {
        Start,
        Dialogue,
        PlayerStartMove,
        PlayerInMovement,
        EnemyMove,
        Paused,
        End
    }

    [Serializable]
    struct EventTime
    {
        public int actionTime;
        public GameObject actionToDo;
    }

    [Header("Tuto")]
    [SerializeField] private DialogueTrigger dialogueTrigger;
    public int tutoPart;

    [Header("Update door when perso healed")]
    [SerializeField] private Sprite doorSprite;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        PlayerPosManager.Init();
        PlayerPosManager.Instance.ListPreviousPlayerPos.Clear();
        PlayerPosManager.Instance.ListPreviousPlayerPos.AddRange(PlayerPosManager.Instance.ListCurrentPlayerPos);
        PlayerPosManager.Instance.ListCurrentPlayerPos.Clear();

        if(previousGameEffect.SameLevel() == false)
        {
            PlayerPosManager.Instance.ListCurrentPlayerPos.Clear();
            PlayerPosManager.Instance.ListPreviousPlayerPos.Clear();
        }

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

        for (int i = 0; i < listDoor.Count;)
        {
            if (listDoor[i] == null)
                listDoor.Remove(listDoor[i]);
            else
                i++;
        }

        

        Debug.Log("<color=gray>[</color><color=#FF00FF>GameManager</color><color=gray>]</color><color=cyan> Event : </color><color=yellow>" + listEvent.Count + "</color><color=cyan> Enemy Door : </color><color=yellow>" + listDoor.Count + "</color><color=cyan> People to Heal : </color><color=yellow>" + listPeopleToHeal.Count + "</color>");
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Level01_tuto")
        {
            //maybe hide player sprite
            dialogueTrigger.TriggerDialogue();
            tutoPart = 1;
        }
        else if(SceneManager.GetActiveScene().name == "Level02_tuto")
        {
            dialogueTrigger.TriggerDialogue();
            tutoPart = 2;
        }
        else
            actualGameState = GameState.PlayerStartMove;
    }

    public void AddPills(int add)
    {
        nbrPills += add;
        pillsText.text = nbrPills.ToString();
    }

    public void RemovePills(int remove)
    {
        MusicList.Instance.PlayPatientHeal();
        nbrPills -= remove;
        pillsText.text = nbrPills.ToString();
        PeopleHeal();
    }

    public void NextAction()
    {
        AddActionPoint();

        //Appeler les fonctions qui doivent se faire � chaque action

        EnemieManager.Instance.Action();

        if(EnemieManager.Instance.cacEnemies.Length + EnemieManager.Instance.rangeEnemies.Length == 0)
            actualGameState = GameState.PlayerStartMove;

        foreach (Event eventEnable in listEventEnable)
        {
            eventEnable.ActionLaunch();
        }

        foreach (GameObject go in listPeopleToHeal)
        {
            var script = go.GetComponent<RemovePillsToHeal>();
            script.CheckPlayer();
        }

        foreach (Door door in listDoor)
        {
            door.Action();
        }


        ActivateEvent();
        sliderMovement.UpdateSlider();
    }

    public void AddActionPoint()
    {
        actionPoint++;
        actionText.text = actionPoint.ToString();
        actionText2.text = actionPoint.ToString();
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
        AchivementSaveManager.Instance.NbPPLHealSave();
        if (peopleToHeal == 0)
        {
            openDoor.GetComponent<SpriteRenderer>().sprite = doorSprite;
            MusicList.Instance.PlayFinalDoorOpen();
            openDoor.layer = 0;
        }
    }

    public void EnemyEndMovement()
    {
        if (actualGameState != GameManager.GameState.End && actualGameState != GameManager.GameState.Paused)
        {
            actualGameState = GameState.PlayerStartMove;
        }
    }

    public void DeathEndGame()
    {
        previousGameEffect._canStartEffect = false;
        playerAnim.SetTrigger("Dead");
        actualGameState = GameState.End;
        StartCoroutine(ActiveDeathMenu());
    }

    IEnumerator ActiveDeathMenu()
    {
        yield return new WaitForSeconds(.85f);
        deathMenu.SetActive(true);
    }

    public void VictoryEndGame()
    {
        AddActionPoint();
        actualGameState = GameState.End;
        playerAnim.SetBool("Walking", false);
        winMenuUI.SetActive(true);
        etoiles[0].SetActive(true);
    }


    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < PlayerPosManager.Instance.ListPreviousPlayerPos.Count - 1; i++)
    //    {
    //        Gizmos.DrawLine(PlayerPosManager.Instance.ListPreviousPlayerPos[i], PlayerPosManager.Instance.ListPreviousPlayerPos[i + 1]);
    //    }
    //}
}
