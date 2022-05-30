using UnityEngine;
using System.Collections.Generic;
using System;
public class Bomb : Event
{
    [Header("Variables")]
    [SerializeField] GameObject _player;
    [SerializeField] Grid _grid;
    [SerializeField] GameObject _warningCube;
    [SerializeField] GameObject _groundBreak;

    [Header("Bomb Options")]
    [Tooltip("Rounds before explosion, if you choose 3, there will be 2 rounds of warning and the bomb will explode next round")]
    [SerializeField] int _actionPointAfterWarning;

    [SerializeField, Range(-30, 30)] int _moveOnX = 0;
    [SerializeField, Range(-30, 30)] int _moveOnY = 0;
    public enum NumberOfPlatform { _1, _2, _4 }
    public NumberOfPlatform numberOfPlatform;

    private enum PlatformAxis { HORIZONTAL, VERTICAL }
    [SerializeField] private PlatformAxis platformAxis;

    private Vector3 _originalCubePos;
    private Vector3 _rightCubePos;
    private Vector3 _upCubePos;
    private Vector3 _upRightCubePos;

    private List<Vector3> _listCubePos = new List<Vector3>();

    private List<GameObject> _listOfWarningCube = new List<GameObject>();
    private bool _hasAlreadyInstantiate = false;

    private void Awake()
    {
        _listCubePos.Clear();
        _listOfWarningCube.Clear();

        _originalCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2, 0);
        _rightCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2 + _grid.cellSize.x, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2);
        _upCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2 + _grid.cellSize.y, 0);
        _upRightCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2 + _grid.cellSize.x, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2 + _grid.cellSize.y, 0);
    }


    public override void ActionLaunch()
    {
        base.ActionLaunch();

        if (!_hasAlreadyInstantiate)
        {
            _hasAlreadyInstantiate = true;
            switch (numberOfPlatform)
            {
                case Bomb.NumberOfPlatform._1:
                    InstantiateWarningCube(_originalCubePos);
                    break;
                case Bomb.NumberOfPlatform._2:
                    if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                    {
                        InstantiateWarningCube(_originalCubePos);
                        InstantiateWarningCube(_rightCubePos);
                    }
                    else
                    {
                        InstantiateWarningCube(_originalCubePos);
                        InstantiateWarningCube(_upCubePos);
                    }

                    break;
                case Bomb.NumberOfPlatform._4:
                    InstantiateWarningCube(_originalCubePos);
                    InstantiateWarningCube(_rightCubePos);
                    InstantiateWarningCube(_upCubePos);
                    InstantiateWarningCube(_upRightCubePos);
                    break;
                default:
                    Debug.LogError("Bomb event switch enter in default");
                    break;
            }
        }

        foreach (GameObject go in _listOfWarningCube)
        {
            if(go != null)
                go.GetComponent<SpriteRenderer>().color = new Color((float)ActionPoint / (float)_actionPointAfterWarning, 0, 0, 1);
        }

        if (ActionPoint == _actionPointAfterWarning)
        {
            LaunchBomb();
        }
    }

    private void LaunchBomb()
    {
        Debug.Log("Bomb explode");
        if (CheckPlayer() == true)
        {
            Debug.Log("Player touch the bomb");
            //Start Anim
            //Block Player Speed || player die
        }
        else 
            DoGroundBreak();
    }

    private void InstantiateWarningCube(Vector3 pos)
    {
        _listCubePos.Add(pos);
        var go = Instantiate(_warningCube, pos, Quaternion.identity);
        go.transform.localScale = new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1);
        _listOfWarningCube.Add(go);
    }


    private bool CheckPlayer()
    {
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                if (Mathf.Abs(Vector3.Distance(_player.transform.position, _originalCubePos))  <= 0.1f ) 
                {
                    return true;
                }
                return false;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    if (Mathf.Abs(Vector3.Distance(_player.transform.position, _originalCubePos)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _rightCubePos)) <= 0.1f)
                    {
                        return true;
                    }
                }
                else if (Mathf.Abs(Vector3.Distance(_player.transform.position, _originalCubePos)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _upCubePos)) <= 0.1)
                {
                    return true;
                }
                return false;
            case Bomb.NumberOfPlatform._4:
                if (Mathf.Abs(Vector3.Distance(_player.transform.position, _originalCubePos)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _rightCubePos)) <= 0.1 || Mathf.Abs(Vector3.Distance(_player.transform.position, _upCubePos)) <= 0.1f || Mathf.Abs(Vector3.Distance(_player.transform.position, _upRightCubePos)) <= 0.1)
                { 
                    return true;
                }
                return false;
            default:
                Debug.LogError("Bomb event switch enter in default");
                return false;
        }
    }

    private void DoGroundBreak()
    {
        foreach (GameObject obj in _listOfWarningCube)
        {
            Destroy(obj);
        }

        _listOfWarningCube.Clear();

        foreach (Vector3 vect in _listCubePos)
        {
            var go = Instantiate(_groundBreak, vect, Quaternion.identity);
            go.transform.localScale = new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1);
        }
    }

    void OnDrawGizmos()
    {
        _originalCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2, 0);
        _rightCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2 + _grid.cellSize.x, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2);
        _upCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2 + _grid.cellSize.y, 0);
        _upRightCubePos = new Vector3(_moveOnX * _grid.cellSize.x + _grid.cellSize.x / 2 + _grid.cellSize.x, _moveOnY * _grid.cellSize.y + _grid.cellSize.y / 2 + _grid.cellSize.y, 0);

        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                Gizmos.DrawCube(_originalCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                break;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    Gizmos.DrawCube(_originalCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                    Gizmos.DrawCube(_rightCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                }
                else
                {
                    Gizmos.DrawCube(_originalCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                    Gizmos.DrawCube(_upCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                }

                break;
            case Bomb.NumberOfPlatform._4:
                Gizmos.DrawCube(_originalCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                Gizmos.DrawCube(_rightCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                Gizmos.DrawCube(_upCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                Gizmos.DrawCube(_upRightCubePos, new Vector3(_grid.cellSize.x, _grid.cellSize.y, 1));
                break;
            default:
                Debug.LogError("Bomb event switch enter in default");
                break;

        }
    }
}
