using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartGame : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject _borderHeight;
    [SerializeField] GameObject _borderHeight2;
    [SerializeField] List<GameObject> _heartList;
    [SerializeField] GameObject _heartPrefab;
    [SerializeField] Transform _heartTransformSpawn;

    [Header("Game Options")]
    [SerializeField] float _gameTime;
    [SerializeField] float _HeartSpeed;
    [SerializeField] float _HeartMinusInterval;
    [SerializeField] float _HeartMaxInterval;

    private float _heartInterval;
    private float _heartCurrentInterval;
    private bool _isGamePlaying = true;

    private void Start()
    {
        _heartInterval = Random.Range(_HeartMinusInterval, _HeartMaxInterval);
        _heartCurrentInterval = _heartInterval;
    }
    // Update is called once per frame
    void Update()
    {
        if(_isGamePlaying)
        {
            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
            {
                _isGamePlaying = false;
            }

            _heartCurrentInterval -= Time.deltaTime;

            if(_heartCurrentInterval < 0)
            {
                _heartCurrentInterval = Random.Range(_HeartMinusInterval, _HeartMaxInterval);
                GameObject go;
                go = Instantiate(_heartPrefab, _heartTransformSpawn.position, Quaternion.identity) as GameObject;
                go.transform.parent = _heartTransformSpawn;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach(GameObject heart in _heartList)
                {
                    if(heart.transform.position.y <= _borderHeight.transform.position.y && heart.transform.position.y >= _borderHeight2.transform.position.y)
                    {
                        SelectHeart();
                    }
                }
            }

        }
    }

    public void Setup()
    {
        Debug.Log("Setup");
        gameUI.SetActive(true);
        StartGame();
    }

    private void StartGame()
    {

    }

    private void SelectHeart()
    {

    }
}
