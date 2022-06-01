using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private GameObject EnemiePrefab;
    private GameObject realDoor;

    private GameObject enemieSpawned;

    [Header("Patern")]
    [SerializeField] [Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("")] private string[] patern;
    [SerializeField] [Tooltip("")] private string[] patrolPatern;


    //ouvre -> sort -> patrouille -> ferme

    public void Action()
    {
        MakeAMove(patern, paternNumber);

        if (paternNumber < patern.Length - 1)
            paternNumber++;
        else
            paternNumber = 0;
    }

    void MakeAMove(string[] _patern, int _paternNumber)
    {
        switch (_patern[_paternNumber])
        {
            case "OD":
                OpenDoor();
                break;

            case "CD":
                CloseDoor();
                break;

            case "EN":
                EnterInTheMap();
                break;

            case "EX":
                ExitTheMap();
                break;

            case "W":
                break;
        }
    }

    void OpenDoor()
    {
       //Open the door
    }

    void CloseDoor()
    {
        //close the door
    }

    void EnterInTheMap()
    {
        //enemieSpawned = Instantiate(EnemiePrefab);
        //enemieSpawned.GetComponent<EnemieBase>().patern = patrolPatern;
    }

    void ExitTheMap()
    {
        //Destroy(enemieSpawned);
    }

}
