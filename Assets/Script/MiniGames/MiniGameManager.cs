using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public enum MiniGameState { NULL = -1, SHERLOCK, BLOOD, HEART}
    public MiniGameState State;

    private void Awake()
    {
        State = MiniGameState.NULL;
    }

    [ContextMenu("Start Sherlock Game")]
    void SetupSherlockGame()
    {
        var sherlockGame = gameObject.GetComponent<SherlockGame>();
        sherlockGame.Setup();
        State = MiniGameState.SHERLOCK;
    }

    [ContextMenu("Start Blood Game")]
    void SetupBloodGame()
    {
        var bloodGame = gameObject.GetComponent<BloodGame>();
        bloodGame.Setup();
        State = MiniGameState.BLOOD;
    }

    [ContextMenu("Start Heart Game")]
    void SetupHeartGame()
    {
        var heartGame = gameObject.GetComponent<HeartGame>();
        heartGame.Setup();
        State = MiniGameState.HEART;
    }

    public void StartSherlockGame()
    {
        var sherlockGame = gameObject.GetComponent<SherlockGame>();
        sherlockGame.StartGame();
    }
}
