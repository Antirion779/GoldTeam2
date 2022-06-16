using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemovePillsToHeal : MonoBehaviour
{
    public int removeCoin;
    public GameObject gameObjectParticule;
    public Canvas canvas;
    public Sprite headSpriteHealed, BodySpriteHealed;
    [SerializeField] private Grid grid; 
    [SerializeField] private TMP_Text text;
    [SerializeField] private SpriteRenderer HeadSpriteRenderer, bodySpriteRenderer;

    private float raycastDistance;
    public LayerMask collisionLayer;

    private int nbPPLToHeal;

    [SerializeField] DialogueManager dialogueManager;

    private void Start()
    {
        if(grid == null || removeCoin == 0)
            Debug.Log("<color=gray>[</color><color=#FF00FF>RemovePillsToHeal</color><color=gray>]</color><color=red> ATTENTION </color><color=#F48FB1> Some object are null </color><color=gray>-</color><color=cyan> Object Name : </color><color=yellow>" + transform.name + "</color><color=cyan> Grid : </color><color=yellow>" + grid + "</color><color=cyan> RemoveCoin : </color><color=yellow>" + removeCoin + "</color>");
        else
        {
            raycastDistance = grid.cellSize.x;
            GameManager.Instance.PeopleToHeal++;
            text.text = removeCoin.ToString();
        }


        nbPPLToHeal = PlayerPrefs.GetInt("NbPPLHealSave", nbPPLToHeal);
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
        if (removeCoin > GameManager.Instance.TotalPills) { }
        else
        {
            GameManager.Instance.RemovePills(removeCoin);
            var effect = Instantiate(gameObjectParticule, transform.position, Quaternion.identity);
            var sprite = gameObject.GetComponent<Sprite>();
            HeadSpriteRenderer.sprite = headSpriteHealed;
            bodySpriteRenderer.sprite = BodySpriteHealed;
            canvas.enabled = false;
            nbPPLToHeal++;
            PlayerPrefs.SetInt("NbPPLHealSave", nbPPLToHeal);
            OpenDialogue();
        }
    }

    private void OpenDialogue()
    {
        if (GameManager.Instance.tutoPart == 1 || dialogueManager != null)
        {
            dialogueManager.UnHideDialogue("Injured soldier");
        }
    }
}
