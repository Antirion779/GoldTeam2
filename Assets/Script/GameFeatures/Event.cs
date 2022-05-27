using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    private int actionPoint;
    public int ActionPoint { get => actionPoint; set => actionPoint = value; }

    public virtual void ActionLaunch()
    {
        actionPoint++;
    }
}
