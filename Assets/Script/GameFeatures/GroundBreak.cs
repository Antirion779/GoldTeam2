using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [SerializeField] private Sprite breakSprite;
    [SerializeField] private Sprite holeSprite;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            GetComponent<SpriteRenderer>().sprite = breakSprite;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 8;
            GetComponent<SpriteRenderer>().sprite = holeSprite;
        }
    }

}
