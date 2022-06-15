using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 startPos, endPos;
    public int pixerDistToDetect = 20;
    private bool fingerDown;

    private  int nbAction;
    private int nbGlissadeSave;

    public LayerMask collisionLayer;
    public float raycastDistance = 2;
    private bool northCollision, southCollision, eastCollision, westCollision;
    private int northModifier, southModifier, eastModifier, westModifier = 1;
    private GameObject actualOilCase;

    private bool isMovementFinish;

    [SerializeField] private Animator anim;

    [SerializeField] private PreviousGameEffect previousGameEffect;

    private void Start()
    {
        endPos = transform.position;      
        PlayerPosManager.Instance.ListCurrentPlayerPos.Add(endPos);
        CheckWall();
        isMovementFinish = true;

        GameManager.Instance.Player = gameObject;
        GameManager.Instance.PlayerAnim = anim;

        if (previousGameEffect == null)
            Debug.Log("<color=gray>[</color><color=#FF00FF>PlayerMovement</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Previous Game Effect : </color><color=yellow>" + previousGameEffect + "</color>");
        nbAction = PlayerPrefs.GetInt("LesActionSave", nbAction);
        
        nbGlissadeSave = PlayerPrefs.GetInt("NbGlissadeSave", nbGlissadeSave);
    }


    private void Update()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
            CheckWall();
        }

        if (fingerDown && isMovementFinish && GameManager.Instance.ActualGameState == GameManager.GameState.PlayerStartMove)
        {
            if (Input.touches[0].position.y >= startPos.y + pixerDistToDetect && !northCollision)
            {
                endPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.GetMoveDistance * northModifier, transform.position.z);
                InMovement(northModifier);
                //Debug.Log("Up");
            }
            else if (Input.touches[0].position.y <= startPos.y - pixerDistToDetect && !southCollision)
            {
                endPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance * southModifier, transform.position.z);
                InMovement(southModifier);
                //Debug.Log("Down");
            }
            else if (Input.touches[0].position.x <= startPos.x - pixerDistToDetect && !westCollision)
            {
                endPos = new Vector3(transform.position.x - GameManager.Instance.GetMoveDistance * westModifier, transform.position.y, transform.position.z);
                InMovement(westModifier);
                transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);
                //Debug.Log("Left");
            }
            else if (Input.touches[0].position.x >= startPos.x + pixerDistToDetect && !eastCollision)
            {
                endPos = new Vector3(transform.position.x + GameManager.Instance.GetMoveDistance * eastModifier, transform.position.y, transform.position.z);
                InMovement(eastModifier);
                transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }


        if (transform.position == endPos && GameManager.Instance.ActualGameState == GameManager.GameState.PlayerInMovement)
        {
            endPos = GetComponent<BoxCenter>().CenterObject();
            transform.position = endPos;
            anim.SetBool("Walking", false);
            CheckWall();

            if (previousGameEffect != null)
            {
                PlayerPosManager.Instance.ListCurrentPlayerPos.Add(endPos);
                previousGameEffect.CheckPlayerMoove(endPos);
            }

            if (GameManager.Instance.ActualGameState != GameManager.GameState.End && GameManager.Instance.ActualGameState != GameManager.GameState.Paused)
            {
                GameManager.Instance.ActualGameState = GameManager.GameState.EnemyMove;
                GameManager.Instance.NextAction();
            }

            isMovementFinish = true;
        }
        else if (transform.position != endPos)
        {
            isMovementFinish = false;
        }

        if(GameManager.Instance.ActualGameState != GameManager.GameState.Paused)
            transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.blue);
       
    }

    private void InMovement(int modifier)
    {
        fingerDown = false;
        GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
        anim.SetBool("Walking", true);

        nbAction++;
        PlayerPrefs.SetInt("LesActionSave", nbAction);
        if (modifier != 1)
        {
            MusicList.Instance.PlayOil();
            nbGlissadeSave++;
            PlayerPrefs.SetInt("NbGlissadeSave", nbGlissadeSave);
            Debug.Log("SAVEEEEDDDDD");

        }
    }

    private void CheckWall()
    {
        northModifier = 1;
        southModifier = 1;
        westModifier = 1;
        eastModifier = 1;

        Check(transform.up, ref northCollision, ref northModifier);
        Check(-transform.up, ref southCollision, ref southModifier);
        Check(-transform.right, ref westCollision, ref westModifier);
        Check(transform.right, ref eastCollision, ref eastModifier);

        //Debug.Log(northCollision + "/" + southCollision + "/" + westCollision + "/" + eastCollision);
    }

    private void Check(Vector3 direction, ref bool isCollision, ref int modifier)
    {
        foreach (GameObject oilCase in GameManager.Instance.OilCaseList)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, oilCase.transform.position)) <= 0.1f)
            {
                oilCase.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                oilCase.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, collisionLayer);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall") || hit.transform.gameObject.layer == LayerMask.NameToLayer("Hole"))
            {
                isCollision = true;
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Oil"))
            {
                isCollision = false;
                bool endCheck = false;
                modifier++;

                while (!endCheck)
                {
                    int oilFound = 0;
                    bool isWall = false;

                    RaycastHit2D[] hitOil = Physics2D.RaycastAll(transform.position, direction, 10.24f * modifier, collisionLayer);
                    Debug.DrawRay(transform.position, direction * raycastDistance * modifier, new Color(255,0,1 * modifier * 10), 2f);
                    
                    foreach (RaycastHit2D hitCol in hitOil)
                    {
                        if (hitCol.transform.gameObject.layer == LayerMask.NameToLayer("Oil"))
                        {
                            oilFound++;
                        }
                        else if (hitCol.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                        {
                            isWall = true;
                        }
                    }

                    if (isWall)
                    {
                        endCheck = true;
                        modifier--;
                    }
                    else if (oilFound == modifier)
                    {
                        modifier++;
                    }
                    else
                    {
                        endCheck = true;
                    }
                }
            }
        }
        else
            isCollision = false;
    }
}
