using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 startPos, endPos;
    public int pixerDistToDetect = 20;
    private bool fingerDown;

    public LayerMask collisionLayer;
    public float raycastDistance = 2;
    private bool northCollision, southCollision, eastCollision, westCollision;

    private bool isMovementFinish, nextAction;

    private void Start()
    {
        endPos = transform.position;
        CheckWall();
        isMovementFinish = true;
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
                fingerDown = false;
                endPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.GetMoveDistance, transform.position.z);
                GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
                //Debug.Log("Up");
            }
            else if (Input.touches[0].position.y <= startPos.y - pixerDistToDetect && !southCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance, transform.position.z);
                GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
                //Debug.Log("Down");
            }
            else if (Input.touches[0].position.x <= startPos.x - pixerDistToDetect && !westCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x - GameManager.Instance.GetMoveDistance, transform.position.y, transform.position.z);
                GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
                //Debug.Log("Left");
            }
            else if (Input.touches[0].position.x >= startPos.x + pixerDistToDetect && !eastCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x + GameManager.Instance.GetMoveDistance, transform.position.y, transform.position.z);
                GameManager.Instance.ActualGameState = GameManager.GameState.PlayerInMovement;
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }

        if (transform.position == endPos && !nextAction && GameManager.Instance.ActualGameState == GameManager.GameState.PlayerInMovement)
        {
            endPos = GetComponent<BoxCenter>().CenterObject();
            transform.position = endPos;

            isMovementFinish = true;
            GameManager.Instance.NextAction();
            GameManager.Instance.ActualGameState = GameManager.GameState.EnemyMove;
            nextAction = true;
        }
        else if (transform.position != endPos)
        {
            isMovementFinish = false;
            nextAction = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.blue);
    }

    private void CheckWall()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.up, raycastDistance, collisionLayer);
        if (hitUp.collider != null)
            northCollision = true;
        else
            northCollision = false;
            
        
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -transform.up, raycastDistance, collisionLayer);
        if (hitDown.collider != null)
            southCollision = true;
        else
            southCollision = false;
        
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, raycastDistance, collisionLayer);
        if (hitLeft.collider != null)
            westCollision = true;
        else
            westCollision = false;

        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.right, raycastDistance, collisionLayer);
        if (hitRight.collider != null)
            eastCollision = true;
        else
            eastCollision = false;

        //Debug.Log(northCollision + "/" + southCollision + "/" + westCollision + "/" + eastCollision);
    }
}
