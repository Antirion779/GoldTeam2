using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]private GameObject enemy;
    private GameObject realDoor;

    [Header("Patern")]
    [SerializeField] [Tooltip("To know where you are in the patrol")] private int paternNumber = 0;
    [SerializeField] [Tooltip("")] private string[] patern;


    //ouvre -> sort -> patrouille -> ferme

    private void Awake()
    {
        enemy.SetActive(false);
    }

    public void Action()
    {
        MakeAMove(patern, paternNumber);

        if (paternNumber < patern.Length - 1)
            paternNumber++;
        else
            paternNumber = 0;

        if (enemy != null)
        {
            if (enemy.activeSelf)
            {
                enemy.GetComponent<EnemieBase>().Action();
            }
        }
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

            case "EX":
                enemy.SetActive(false);
                break;

            case "W":
                break;
        }
    }

    private void Update()
    {
        Debug.Log(enemy);
    }
}
