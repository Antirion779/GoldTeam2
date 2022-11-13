using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision )
    {
        if(collision.CompareTag("Player"))
        {
            MusicList.Instance.PlayEndLevel();
            GameManager.Instance.VictoryEndGame();
            Achievement.Instance.Setup();
            Leaderboard.Instance.getLeaderboardScore();
        }
    }
    

    /*
    15 Decor où le joueur passe dessous

    10/11/12/13 Player
    10/11/12/13 Ennemie
    8/9 Blesser
    6/7 Porte Ennemie
    5 Porte de fin
    5 Decoration sur les Mur
    4 Mur
    3 Pillule
    2 Trou
    
    1                    Huile
    1 Bomb Case prévention   
    0 Sol
    */
}
