using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public static Achievement Instance;

    public int allAction = 0;
    [SerializeField, Tooltip("Cacaaca")]
    public int etoile1, etoile2, etoile3;
    
   [SerializeField] private Image[] imgEtoile;

    public Sprite spriteEtoile1;
    public Sprite spriteEtoile2;
    public Sprite spriteEtoile3;

    

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
          
            imgEtoile[2].sprite = spriteEtoile1;

        
        }
        if (allAction <= etoile2)
        {
           
            imgEtoile[1].sprite = spriteEtoile2;
           
        }
        if (allAction <= etoile1)
        {
            imgEtoile[0].sprite = spriteEtoile3;
        }
    }
}
