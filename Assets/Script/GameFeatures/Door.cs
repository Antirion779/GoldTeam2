using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private GameObject enemy;
    private GameObject realDoor;

    [Header("Patern")]
    [SerializeField] [Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("TL/TR -> Rotation, EN/EX -> player apparition, CL/OP -> Close & Open the door + enemy Move, W -> Enemy can move")] private string[] patern;

    private string firstPatern;


    //ouvre -> sort -> patrouille -> ferme

    private void Awake()
    {
        enemy.SetActive(false);
        foreach (string paternAct in patern)
        {
            if (paternAct == "TL" || paternAct == "TR")
            {
                firstPatern = paternAct;
                return;
            }
        }
    }

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
            case "TL":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                break;

            case "TR":
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
                break;

            case "EN":
                enemy.SetActive(true);
                break;

            case "CL":
                ExitDoor(false);
                enemy.GetComponent<EnemieBase>().Action();
                break;

            case "OP":
                ExitDoor(true);
                enemy.GetComponent<EnemieBase>().Action();
                break;

            case "EX":
                enemy.SetActive(false);
                ExitDoor(false);
                break;

            case "W":
                enemy.GetComponent<EnemieBase>().Action();
                break;
        }
    }

    //On inverse la rotation pour fermer la porte
    private void ExitDoor(bool invert)
    {
        switch (firstPatern)
        {
            case "TR":
                if(!invert)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                else 
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
                break;

            case "TL":
                if (!invert)
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
                else
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                break;
        }
    }
}
