using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCenter : MonoBehaviour
{
    private void Start()
    {
        Vector2 startPos = transform.position;
        float blockDistance = GameManager.Instance.GetMoveDistance;
        float halfBlockDistance = GameManager.Instance.GetMoveDistance / 2;
        transform.position = new Vector3((int)(startPos.x / blockDistance) * blockDistance, (int)(startPos.y / blockDistance) * blockDistance);

        if (startPos.x >= 0)
            transform.position = new Vector3(transform.position.x + halfBlockDistance, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x - halfBlockDistance, transform.position.y);

        if (startPos.y >= 0)
            transform.position = new Vector3(transform.position.x, transform.position.y + halfBlockDistance);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y - halfBlockDistance);
        
    }
}

