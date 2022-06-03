using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject doorOpen, doorSpriteObject;
    [SerializeField] private Sprite doorOpenSprite, doorCloseSprite;

    [Header("Patern")]
    [SerializeField] [Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("OP/CL -> Open/Close the door, EN/EX -> player apparition, CLA/OPA -> Close & Open the door + enemy Move, W -> Enemy can move")] private string[] patern;


    //ouvre -> sort -> patrouille -> ferme

    private void Awake()
    {
        enemy.SetActive(false);
        doorOpen.SetActive(false);
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
            case "OP":
                ActivateDoor(true);
                break;

            case "CL":
                ActivateDoor(false);
                break;

            case "EN":
                enemy.SetActive(true);
                break;

            case "CLA":
                ActivateDoor(false);
                enemy.GetComponent<EnemieBase>().Action();
                break;

            case "OPA":
                ActivateDoor(true);
                enemy.GetComponent<EnemieBase>().Action();
                break;

            case "EX":
                enemy.SetActive(false);
                ActivateDoor(false);
                break;

            case "W":
                enemy.GetComponent<EnemieBase>().Action();
                break;
        }
    }

    //On inverse la rotation pour fermer la porte
    private void ActivateDoor(bool invert)
    {
        if (invert)
        {
            doorSpriteObject.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
            doorOpen.SetActive(true);
        }
        else
        {
            doorSpriteObject.GetComponent<SpriteRenderer>().sprite = doorCloseSprite;
            doorOpen.SetActive(false);
        }
    }
}
