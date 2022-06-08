using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovePillsToHeal : MonoBehaviour
{
    public int removeCoin;

    [SerializeField] private Grid grid; 
    [SerializeField] private TMP_Text text;

    private Vector3 _downCube;
    private Vector3 _upCube;
    private Vector3 _rightCube;
    private Vector3 _leftCube;

    private void Start()
    {
        if(grid == null || removeCoin == 0)
            Debug.Log("<color=gray>[</color><color=#FF00FF>RemovePillsToHeal</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Grid : </color><color=yellow>" + grid + "</color><color=cyan> RemoveCoin : </color><color=yellow>" + removeCoin + "</color>");
        else
        {
            _downCube = new Vector3(transform.position.x, transform.position.y - grid.cellSize.y);
            _upCube = new Vector3(transform.position.x, transform.position.y + grid.cellSize.y);
            _rightCube = new Vector3(transform.position.x + grid.cellSize.x, transform.position.y);
            _leftCube = new Vector3(transform.position.x - grid.cellSize.x, transform.position.y);
            GameManager.Instance.PeopleToHeal++;
            text.text = removeCoin.ToString();
        }
    }
    public void CheckPlayer()
    {
        GameObject player = GameManager.Instance.Player;

        if (player != null)
        {
            if (Mathf.Abs(Vector3.Distance(player.transform.position, _downCube)) <= 0.1f || Mathf.Abs(Vector3.Distance(player.transform.position, _upCube)) <= 0.1f)
            {
                UpdatePills();
            }
            else if (Mathf.Abs(Vector3.Distance(player.transform.position, _rightCube)) <= 0.1f || Mathf.Abs(Vector3.Distance(player.transform.position, _leftCube)) <= 0.1f)
            {
                UpdatePills();
            }
        }
    }


    private void UpdatePills()
    {
        if (removeCoin > GameManager.Instance.TotalPills)
        {

            //Debug.Log("TA LOOSEEE");
        }
        else
        {
            GameManager.Instance.RemovePills(removeCoin);
            //Debug.Log("TA WINNNNNN");
            text.text = "";
        }
    }
}
