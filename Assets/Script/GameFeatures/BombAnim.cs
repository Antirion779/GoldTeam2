using UnityEngine;

public class BombAnim : MonoBehaviour
{
    private Bomb _bomb;
    public void UpdateAnim()
    {
        if(_bomb != null && _bomb.playerWillDie)
        {
            GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("Dead");
            GameManager.Instance.DeathEndGame();
        }
        else
        {
            _bomb.DoGroundBreak();
            _bomb.SetAchievement();
        }
    }

    public void PlayerState(Bomb bomb)
    {
        _bomb = bomb;
    }

}
