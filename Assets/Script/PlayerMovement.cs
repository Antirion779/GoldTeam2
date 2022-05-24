using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 startPos;
    public int pixerDistToDetect = 20;
    private bool fingerDown;
    public float moveSpeed = 5;
    public float moveDistance = 1;

    private void Update()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.y >= startPos.y + pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Up");
            }
            else if (Input.touches[0].position.y <= startPos.y - pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Down");
            }
            else if (Input.touches[0].position.x <= startPos.x - pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Left");
            }
            else if (Input.touches[0].position.x >= startPos.x + pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            fingerDown = false;

            //TESTING FOR PC

        if (!fingerDown && Input.touchCount > 0 && Input.GetMouseButtonDown(0))
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.mousePosition.y >= startPos.y + pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Up");
            }
            else if (Input.mousePosition.y <= startPos.y - pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Down");
            }
            else if (Input.mousePosition.x <= startPos.x - pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Left");
            }
            else if (Input.mousePosition.x >= startPos.x + pixerDistToDetect)
            {
                fingerDown = false;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
                //Debug.Log("Right");
            }
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
            fingerDown = false;
    }
}
