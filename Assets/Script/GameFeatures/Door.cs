using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private GameObject EnemiePrefab;
    private GameObject realDoor;
    private bool isOpen;
    private bool hasToChange;
    private Vector3 endPos;

    //ouvre -> sort -> patrouille -> ferme

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 _newDir = Vector3.RotateTowards();
        //transform.rotation = Quaternion.LookRotation();
    }

    public void Action()
    {

    }

    void ChangeDoor()
    {
        if (isOpen && hasToChange)
            endPos = new Vector3(transform.rotation.x, transform.rotation.y, 0);
    }
}
