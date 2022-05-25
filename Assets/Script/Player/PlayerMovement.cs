using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 startPos, endPos;
    public int pixerDistToDetect = 20;
    private bool fingerDown;

    public LayerMask collisionLayer;
    public float raycastDistance = 2;
    private bool northCollision, southCollision, eastCollision, westCollision;

    private void Start()
    {
        CheckWall();
    }


    private void Update()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
            CheckWall();
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.y >= startPos.y + pixerDistToDetect && !northCollision)
            {
                fingerDown = false;
                endPos = new Vector2(transform.position.x, transform.position.y + GameManager.Instance.GetMoveDistance);
                //Debug.Log("Up");
            }
            else if (Input.touches[0].position.y <= startPos.y - pixerDistToDetect && !southCollision)
            {
                fingerDown = false;
                endPos = new Vector2(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance);
                //Debug.Log("Down");
            }
            else if (Input.touches[0].position.x <= startPos.x - pixerDistToDetect && !westCollision)
            {
                fingerDown = false;
                endPos = new Vector2(transform.position.x - GameManager.Instance.GetMoveDistance, transform.position.y);
                //Debug.Log("Left");
            }
            else if (Input.touches[0].position.x >= startPos.x + pixerDistToDetect && !eastCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x + GameManager.Instance.GetMoveDistance, transform.position.y);
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }

            //TESTING FOR PC

        if (!fingerDown && Input.touchCount > 0 && Input.GetMouseButtonDown(0))
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
            CheckWall();
        }

        if (fingerDown)
        {
            if (Input.mousePosition.y >= startPos.y + pixerDistToDetect && !northCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x, transform.position.y + GameManager.Instance.GetMoveDistance);
                //Debug.Log("Up");
            }
            else if (Input.mousePosition.y <= startPos.y - pixerDistToDetect && !southCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x, transform.position.y - GameManager.Instance.GetMoveDistance);
                //Debug.Log("Down");
            }
            else if (Input.mousePosition.x <= startPos.x - pixerDistToDetect && !westCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x - GameManager.Instance.GetMoveDistance, transform.position.y);
                //Debug.Log("Left");
            }
            else if (Input.mousePosition.x >= startPos.x + pixerDistToDetect && !eastCollision)
            {
                fingerDown = false;
                endPos = new Vector3(transform.position.x + GameManager.Instance.GetMoveDistance, transform.position.y);
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
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
