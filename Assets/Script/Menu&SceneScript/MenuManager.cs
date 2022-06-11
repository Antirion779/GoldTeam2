using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public string sceneName;

    private float endAnimTime;

    [SerializeField] private Animator pauseAnimator, deathAnimator;

    private GameManager.GameState ancienState;

    private void Awake()
    {
        endAnimTime = 0.5f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void SettingsButton()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Resume();
        }
        else
        {
            gameIsPaused = true;
            Paused();
        }
    }

    public void Paused()
    {
        ancienState = GameManager.Instance.ActualGameState;
        GameManager.Instance.ActualGameState = GameManager.GameState.Paused;
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        TriggerExitAnimation();
        StartCoroutine(ResumeTime());
    }

    private IEnumerator ResumeTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        GameManager.Instance.ActualGameState = ancienState;
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
    }

    public void ResetGame()
    {
        TriggerExitAnimation();
        StartCoroutine(ResetTime());
    }

    private IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
    }

    public void LoadScene()
    {
        TriggerExitAnimation();
        StartCoroutine(LoadSceneTime());
    }

    private IEnumerator LoadSceneTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex, sceneName);
        SceneManager.LoadScene(sceneName);
        gameIsPaused = false;
    }

    private void TriggerExitAnimation()
    {
        if (pauseAnimator.isActiveAndEnabled)
            pauseAnimator.SetTrigger("Exit");
        if (deathAnimator.isActiveAndEnabled)
            deathAnimator.SetTrigger("Exit");
    }
}