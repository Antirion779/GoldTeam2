using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovePillsToHeal : MonoBehaviour
{
    public int removeCoin;

    [SerializeField] private Grid grid; 
    [SerializeField] private TMP_Text text;

    private float raycastDistance;
    public LayerMask collisionLayer;

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
            raycastDistance = grid.cellSize.x;
            GameManager.Instance.PeopleToHeal++;
            text.text = removeCoin.ToString();
        }
    }
    public void CheckPlayer()
    {
        GameObject player = GameManager.Instance.Player;

        Debug.DrawRay(transform.position, transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, -transform.right * raycastDistance, Color.blue);
        Debug.DrawRay(transform.position, transform.right * raycastDistance, Color.blue);

        Check(transform.up);
        Check(-transform.up);
        Check(-transform.right);
        Check(transform.right);

        //if (player != null)
        //{
        //    if (Mathf.Abs(Vector3.Distance(player.transform.position, _downCube)) <= 0.2f || Mathf.Abs(Vector3.Distance(player.transform.position, _upCube)) <= 0.2f)
        //    {
        //        UpdatePills();
        //    }
        //    else if (Mathf.Abs(Vector3.Distance(player.transform.position, _rightCube)) <= 0.2f || Mathf.Abs(Vector3.Distance(player.transform.position, _leftCube)) <= 0.2f)
        //    {
        //        UpdatePills();
        //    }
        //}

    }

    private void Check(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, collisionLayer);
        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
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
