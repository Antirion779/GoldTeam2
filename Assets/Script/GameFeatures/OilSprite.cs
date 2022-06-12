using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;

public class OilSprite : MonoBehaviour
{
    [SerializeField] private Sprite NSEW, SE, SEW, SW, NSE, NSW, NE, NEW, NW, EW, NS, N, S, E, W, nothing;

    private bool northContact, southContact, eastContact, westContact;

    [SerializeField] private LayerMask oilLayer;

    private void Start()
    {
        CheckOil(transform.up, ref northContact);
        CheckOil(-transform.up, ref southContact);
        CheckOil(transform.right, ref eastContact);
        CheckOil(-transform.right, ref westContact);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (northContact)
        {
            if (southContact)
            {
                if (eastContact)
                {
                    if (westContact)
                        spriteRenderer.sprite = NSEW;
                    else
                        spriteRenderer.sprite = NSE;
                }
                else
                {
                    if (westContact)
                        spriteRenderer.sprite = NSW;
                    else
                        spriteRenderer.sprite = NS;
                }
            }
            else
            {
                if (eastContact)
                {
                    if (westContact)
                        spriteRenderer.sprite = NEW;
                    else
                        spriteRenderer.sprite = NE;
                }
                else
                {
                    if (westContact)
                        spriteRenderer.sprite = NW;
                    else
                        spriteRenderer.sprite = N;
                }
            }
        }
        else
        {
            if (southContact)
            {
                if (eastContact)
                {
                    if (westContact)
                        spriteRenderer.sprite = SEW;
                    else
                        spriteRenderer.sprite = SE;
                }
                else
                {
                    if (westContact)
                        spriteRenderer.sprite = SW;
                    else
                        spriteRenderer.sprite = S;
                }
            }
            else
            {
                if (eastContact)
                {
                    if (westContact)
                        spriteRenderer.sprite = EW;
                    else
                        spriteRenderer.sprite = E;
                }
                else
                {
                    if (westContact)
                        spriteRenderer.sprite = W;
                    else
                        spriteRenderer.sprite = nothing;
                }
            }
        }
    }

    private void CheckOil(Vector3 direction, ref bool isCollision)
    {
        Debug.DrawRay(transform.position, direction * 10.24f, Color.green, 10);
        RaycastHit2D[] hitAll = Physics2D.RaycastAll(transform.position, direction, 10.24f, oilLayer);
        Debug.Log(hitAll.Length);
        if (hitAll.Length == 2)
        {
            isCollision = true;
        }
    }
}
