using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovePillsToHeal : MonoBehaviour
{
    public int removeCoin;

    [SerializeField] private Grid _grid;
    [SerializeField] private GameObject _player;
    [SerializeField] private TMP_Text _text;

    private Vector3 _downCube;
    private Vector3 _upCube;
    private Vector3 _rightCube;
    private Vector3 _leftCube;

    private void Start()
    {
        _downCube = new Vector3(transform.position.x, transform.position.y - _grid.cellSize.y);
        _upCube = new Vector3(transform.position.x, transform.position.y + _grid.cellSize.y);
        _rightCube = new Vector3(transform.position.x + _grid.cellSize.x, transform.position.y);
        _leftCube = new Vector3(transform.position.x - _grid.cellSize.x, transform.position.y);
        GameManager.Instance.PeopleToHeal++;
        _text.text = removeCoin.ToString();
    }
    public void CheckPlayer()
    {
        if (Mathf.Abs(Vector3.Distance(_player.transform.position, _downCube)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _upCube)) <= 0.1f)
        {
            UpdatePills();
        }
        else if (Mathf.Abs(Vector3.Distance(_player.transform.position, _rightCube)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _leftCube)) <= 0.1f)
        {
            UpdatePills();
        }
    }


    private void UpdatePills()
    {
        if (removeCoin > GameManager.Instance.TotalPills)
        {

            Debug.Log("TA LOOSEEE");
        }
        else
        {
            GameManager.Instance.RemovePills(removeCoin);
            Debug.Log("TA WINNNNNN");

        }
    }
}
