using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    public string selectorSceneName;
    public string nextSceneName;

    private float endAnimTime;

    [SerializeField] private Animator pauseAnimator, deathAnimator, fadeAnimator, victoryAnimator;

    private GameManager.GameState ancienState;

    [SerializeField] private GameObject canvasDialogue;
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        endAnimTime = 0.5f;
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
        gameIsPaused = true;
        MusicManager.instance.SetAnimation();
        if (canvasDialogue != null)
            canvasDialogue.SetActive(false);
    }

    public void Resume()
    {
        TriggerExitAnimation();
        StartCoroutine(ResumeTime());

        if (canvasDialogue != null)
        {
            canvasDialogue.SetActive(true);
            dialogueManager._animator.SetBool("IsOpen", true);
        }
            
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
        gameIsPaused = false;
        TriggerExitAnimation();
        StartCoroutine(ResetTime());
    }

    private IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
    }

    public void ResetGameAndSave()
    {
        gameIsPaused = false;
        TriggerExitAnimation();
        StartCoroutine(ResetAndSaveTime());
    }

    private IEnumerator ResetAndSaveTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex, nextSceneName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
    }

    public void LoadSelectorScene()
    {
        gameIsPaused = false;
        TriggerExitAnimation();
        StartCoroutine(LoadSelectorSceneTime());
    }

    private IEnumerator LoadSelectorSceneTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex, selectorSceneName);
        SceneManager.LoadScene(selectorSceneName);
    }

    public void LoadNextScene()
    {
        gameIsPaused = false;
        TriggerExitAnimation();
        StartCoroutine(LoadNextSceneTime());
    }

    private IEnumerator LoadNextSceneTime()
    {
        yield return new WaitForSeconds(endAnimTime);

        LoadAndSaveData.instance.SaveData(SceneManager.GetActiveScene().buildIndex, nextSceneName);

        GameManager.Instance.winMenuUI.SetActive(false);
        GameManager.Instance.etoiles[0].SetActive(false);
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadSelectSceneForFirstScene()
    {
        SceneManager.LoadScene(selectorSceneName);
    }

    public void GoToLevelSelectorWithOutSaveZer()
    {
        gameIsPaused = false;
        TriggerExitAnimation();
        StartCoroutine(GoToLevelSelectorWithOutSaveZerTime());
    }

    private IEnumerator GoToLevelSelectorWithOutSaveZerTime()
    {
        yield return new WaitForSeconds(endAnimTime);
        SceneManager.LoadScene(selectorSceneName);
    }

    private void TriggerExitAnimation()
    {
        if (pauseAnimator.isActiveAndEnabled )
            pauseAnimator.SetTrigger("Exit");
        if (deathAnimator.isActiveAndEnabled)
            deathAnimator.SetTrigger("Exit");
        if(victoryAnimator.isActiveAndEnabled)
            victoryAnimator.SetTrigger("Exit");
        if(fadeAnimator.isActiveAndEnabled && !gameIsPaused)
            fadeAnimator.SetTrigger("FadeOut");
    }
}