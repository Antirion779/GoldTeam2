using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeartGame : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject borderHeight;
    [SerializeField] GameObject borderHeight2;
    [SerializeField] List<GameObject> _HeartList;
    [SerializeField] GameObject HeartPrefab;
    [SerializeField] Transform HeartTransformSpawn;

    [Header("Game Options")]
    [SerializeField] float _gameTime;
    [SerializeField] float _HeartSpeed;
    [SerializeField] float _HeartMinusInterval;
    [SerializeField] float _HeartMaxInterval;
    private float _HeartInterval;
    public float _HeartCurrentInterval;
    private bool isGamePlaying = true;

    private void Start()
    {
        _HeartInterval = Random.Range(_HeartMinusInterval, _HeartMaxInterval);
        _HeartCurrentInterval = _HeartInterval;
    }
    // Update is called once per frame
    void Update()
    {
        if(isGamePlaying)
        {
            _gameTime -= Time.deltaTime;
            _text.text = _gameTime.ToString("0.0");

            if (_gameTime < 0)
            {
                isGamePlaying = false;
            }

            _HeartCurrentInterval -= Time.deltaTime;

            if(_HeartCurrentInterval < 0)
            {
                _HeartCurrentInterval = Random.Range(_HeartMinusInterval, _HeartMaxInterval);
                GameObject go;
                go = Instantiate(HeartPrefab, HeartTransformSpawn.position, Quaternion.identity) as GameObject;
                go.transform.parent = HeartTransformSpawn;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                foreach(GameObject heart in _HeartList)
                {
                    if(heart.transform.position.y <= borderHeight.transform.position.y && heart.transform.position.y >= borderHeight2.transform.position.y)
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
