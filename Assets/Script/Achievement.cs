using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    private int allAction = GameManager.Instance.ActionPoint;
    [SerializeField, Tooltip("Cacaaca")]
    private int etoile1, etoile2, etoile3;
    
   [SerializeField] private Image[] imgEtoile;
    public Sprite spriteEtoile;
   
    private void Start()
    {
     
    }

    void Update()
    {
        actionAchievement();
    }

    public void actionAchievement()
    {
        if (allAction <= etoile3)
        {
            imgEtoile[2].sprite = spriteEtoile;
        
        }
        else if (allAction <= etoile2)
        {
            imgEtoile[1].sprite = spriteEtoile;
           
        }
        else
        {
            imgEtoile[0].sprite = spriteEtoile;
        }
    }
}
