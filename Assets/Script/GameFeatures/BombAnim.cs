using UnityEngine;

public class BombAnim : MonoBehaviour
{
    private Bomb _bomb;
    [SerializeField]
    private Animator Anim1, Anim2;

    public void UpdateAnim()
    {
        if(_bomb != null && _bomb.playerWillDie)
        {
            GameManager.Instance.Player.GetComponentInChildren<Animator>().SetTrigger("Dead");
            GameManager.Instance.DeathEndGame();
        }
        else
        {
            _bomb.ExposeGroundBreak();
            _bomb.SetAchievement();
        }
        if (MusicManager.instance.isVibrationEnabled)
            Handheld.Vibrate();
        Destroy(gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance.ActualGameState == GameManager.GameState.Paused)
        {
            Anim1.speed = 0;
            Anim2.speed = 0;
        }
        else
        {
            Anim1.speed = 1;
            Anim2.speed = 1;
        }
    }

    public void PlayerState(Bomb bomb)
    {
        _bomb = bomb;
    }

}
