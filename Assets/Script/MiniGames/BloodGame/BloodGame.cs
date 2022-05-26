using UnityEngine;
using TMPro;

public class BloodGame : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] GameObject _gameUI;
    [SerializeField] GameObject _borderHeight;
    [SerializeField] GameObject _borderHeight2;
    [SerializeField] GameObject _blood;
    [SerializeField] TMP_Text _text;

    [Header("Game Options")]
    [SerializeField] float _gameTime;
    [SerializeField] float _inputIncreaseHeight = 10;
    [SerializeField] float _bloodSpeed;

    private bool _isGamePlaying = false;
    private bool _isInsideBox = false;
    private void Update()
    {
        if(_isGamePlaying)
        {
            _blood.transform.position -= new Vector3(0, _bloodSpeed * Time.deltaTime);
            if (_blood.transform.position.y <= _borderHeight.transform.position.y && _blood.transform.position.y >= _borderHeight2.transform.position.y)
                _isInsideBox = true;
            else
                _isInsideBox = false;

            if (Input.GetKeyDown(KeyCode.Mouse0))
                    BloodUp();

            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
            {
                _isGamePlaying = false;

                if(_isInsideBox)
                    Win();
                else
                    Loss();
            }
        }
    }

    public void Setup()
    {
        Debug.Log("Setup");
        _gameUI.SetActive(true);
        StartGame();
    }

    private void StartGame()
    {
        _isGamePlaying = true;
    }

    public void Win()
    {
        Debug.Log("Win");
    }

    public void Loss()
    {
        Debug.Log("Loss");
    }
    private void BloodUp()
    {
        _blood.transform.position += new Vector3(0, _inputIncreaseHeight, 0);
    }
}
