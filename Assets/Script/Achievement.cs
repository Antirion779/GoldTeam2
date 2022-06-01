using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public static Achievement Instance;

    public int allAction = 0;
    [SerializeField, Tooltip("Cacaaca")]
    private int etoile1, etoile2, etoile3;
    
   [SerializeField] private Image[] imgEtoile;
    public Sprite spriteEtoile;
   
    private void Awake()
    {
       
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        allAction = GameManager.Instance.ActionPoint;
        actionAchievement();
    }

    public void actionAchievement()
    {
        if (allAction <= etoile3)
        {
            imgEtoile[2].sprite = spriteEtoile;

        
        }
        if (allAction <= etoile2)
        {
            imgEtoile[1].sprite = spriteEtoile;
           
        }
        if (allAction <= etoile1)
        {
            imgEtoile[0].sprite = spriteEtoile;
        }
    }
}
