using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BloodGame : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject borderHeight;
    [SerializeField] GameObject borderHeight2;
    [SerializeField] GameObject blood;
    [SerializeField] TMP_Text _text;

    [SerializeField] float _gameTime;
    [SerializeField] float _bloodTime;
    private bool isGamePlaying = false;

    public bool isInsideBox = false;
    private void Update()
    {
        if(isGamePlaying)
        {
            blood.transform.position -= new Vector3(0, _bloodTime * Time.deltaTime);
            if (blood.transform.position.y <= borderHeight.transform.position.y && blood.transform.position.y >= borderHeight2.transform.position.y)
                isInsideBox = true;
            else
                isInsideBox = false;

            if (Input.GetKeyDown(KeyCode.Mouse0))
                    BloodUp();

            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
            {
                isGamePlaying = false;

                if(isInsideBox)
                    Win();
                else
                    Loss();
            }
        }
    }

    public void Setup()
    {
        Debug.Log("Setup");
        gameUI.SetActive(true);
        StartGame();
    }

    public void StartGame()
    {
        isGamePlaying = true;
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
        blood.transform.position += new Vector3(0, 10, 0);
    }
}
