using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] float _gameTime;
    private bool isGamePlaying = false;


    private void Update()
    {
        if(isGamePlaying)
        {
            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
                Loss();
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
        isGamePlaying = true;
    }
    public void FindObject()
    {
        _buttonObjectToFind.GetComponent<Button>().enabled = false;
        Win();
    }

    private void Win()
    {
        isGamePlaying = false;
        Debug.Log("You Win");
    }

    private void Loss()
    {
        isGamePlaying = false;
        Debug.Log("You Loss");
    }
}
