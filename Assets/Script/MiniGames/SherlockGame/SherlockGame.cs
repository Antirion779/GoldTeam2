using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SherlockGame : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] Button _buttonObjectToFind;
    [SerializeField] GameObject _gameUI;
    [SerializeField] TMP_Text _text;
    [SerializeField] Image _backgroundImage;
    [SerializeField] Image _objectToFindImage;

    [Header("Game Options")]
    [SerializeField] float _gameTime;

    private bool _isGamePlaying = false;


    private void Update()
    {
        if(_isGamePlaying)
        {
            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
            {
                Loss();
                FinishGame();
            }
                
                
        }
    }

    public void Setup()
    {
        Debug.Log("Setup");
        _gameUI.SetActive(true);
    }

    public void StartGame()
    {
        _backgroundImage.color = new Color(_backgroundImage.color.r, _backgroundImage.color.g, 1f);
        _objectToFindImage.color = new Color(_objectToFindImage.color.r, _objectToFindImage.color.g, 1f);
        _isGamePlaying = true;
    }
    public void FindObject()
    {
        _buttonObjectToFind.GetComponent<Button>().enabled = false;
        Win();
        FinishGame();
    }

    private void Win()
    {
        _isGamePlaying = false;
        Debug.Log("You Win");
    }

    private void Loss()
    {
        _isGamePlaying = false;
        Debug.Log("You Loss");
    }

    private void FinishGame()
    {
        var miniGameManager = gameObject.GetComponent<MiniGameManager>();
        miniGameManager.State = MiniGameManager.MiniGameState.NULL;
    }
}
