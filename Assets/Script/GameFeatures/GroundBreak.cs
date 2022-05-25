using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [SerializeField] private Sprite holeSprite;

    private void OnTriggerExit2D(Collider2D col)
    {
        gameObject.layer = 3;
        GetComponent<SpriteRenderer>().sprite = holeSprite;
    }

}
