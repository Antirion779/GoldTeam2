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
    [SerializeField] [Tooltip("OP/CL -> Open/Close the door, EN/EX -> player apparition, CLA/OPA -> Close & Open the door + enemy Move, W -> Enemy can move")] private List<string> patern = new List<string>();


    //ouvre -> sort -> patrouille -> ferme

    private void Awake()
    {
        for (int i = 0; i < patern.Count;)
        {
            if (patern[i] == null)
                patern.Remove(patern[i]);
            else
                i++;
        }

        if (enemy == null || patern.Count == 0)
            Debug.Log("<color=gray>[</color><color=#FF00FF>Door</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Enemy : </color><color=yellow>" + enemy + "</color><color=cyan> Number of Patern : </color><color=yellow>" + patern.Count + "</color>");
        else 
            enemy.SetActive(false);

        doorOpen.SetActive(false);
    }

    public void Action()
    {
        MakeAMove(patern, paternNumber);

        if (paternNumber < patern.Count - 1)
            paternNumber++;
        else
            paternNumber = 0;
    }

    void MakeAMove(List<string> _patern, int _paternNumber)
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
                if(enemy.GetComponent<EnemieCac>() != null)
                    enemy.GetComponent<EnemieCac>().Action();
                else if (enemy.GetComponent<EnemieRange>() != null)
                    enemy.GetComponent<EnemieRange>().Action();
                break;

            case "OPA":
                ActivateDoor(true);
                if (enemy.GetComponent<EnemieCac>() != null)
                    enemy.GetComponent<EnemieCac>().Action();
                else if (enemy.GetComponent<EnemieRange>() != null)
                    enemy.GetComponent<EnemieRange>().Action();
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
