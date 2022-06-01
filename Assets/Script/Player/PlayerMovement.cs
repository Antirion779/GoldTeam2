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

    private bool isMovementFinish;

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


        if (transform.position == endPos && GameManager.Instance.ActualGameState == GameManager.GameState.PlayerInMovement)
        {
            endPos = GetComponent<BoxCenter>().CenterObject();
            transform.position = endPos;

            isMovementFinish = true;
            GameManager.Instance.NextAction();
            GameManager.Instance.ActualGameState = GameManager.GameState.EnemyMove;
        }
        else if (transform.position != endPos)
        {
            isMovementFinish = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, endPos, GameManager.Instance.GetMoveSpeed * Time.deltaTime);

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.blue);
    }

    private void CheckWall()
    {
        Check(transform.up, ref northCollision);
        Check(-transform.up, ref southCollision);
        Check(-transform.right, ref westCollision);
        Check(transform.right, ref eastCollision);

        //Debug.Log(northCollision + "/" + southCollision + "/" + westCollision + "/" + eastCollision);
    }

    private void Check(Vector3 direction, ref bool isCollision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, collisionLayer);
        if (hit.collider != null)
        {
            Transform jeanmich = hit.transform;
            Debug.Log(jeanmich.position);

        }
        else
            isCollision = false;

        //if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
        //{
        //    isCollision = true;
        //}
        //else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Oil"))
        //{
        //    //Here comes the problem
        //}
        //else
        //    isCollision = false;
    }
}
