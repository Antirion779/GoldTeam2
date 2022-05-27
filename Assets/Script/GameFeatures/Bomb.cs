using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
public class Bomb : Event
{
    [Header("Variables")]
    [SerializeField] GameObject _player;
    [SerializeField] Grid grid;
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

        _originalCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2, 0);
        _rightCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2 + grid.cellSize.x, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2);
        _upCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2 + grid.cellSize.y, 0);
        _upRightCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2 + grid.cellSize.x, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2 + grid.cellSize.y, 0);
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
                    _listCubePos.Add(_originalCubePos);
                    var go = Instantiate(_warningCube, _originalCubePos, Quaternion.identity);
                    go.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                    _listOfWarningCube.Add(go);
                    break;
                case Bomb.NumberOfPlatform._2:
                    if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                    {
                        _listCubePos.Add(_originalCubePos);
                        var go1 = Instantiate(_warningCube, _originalCubePos, Quaternion.identity);
                        go1.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                        _listOfWarningCube.Add(go1);
                        _listCubePos.Add(_rightCubePos);
                        var go2 = Instantiate(_warningCube, _rightCubePos, Quaternion.identity);
                        go2.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                        _listOfWarningCube.Add(go2);
                    }
                    else
                    {
                        _listCubePos.Add(_originalCubePos);
                        var go3 = Instantiate(_warningCube, _originalCubePos, Quaternion.identity);
                        go3.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                        _listOfWarningCube.Add(go3);
                        _listCubePos.Add(_upCubePos);
                        var go4 = Instantiate(_warningCube, _upCubePos, Quaternion.identity);
                        go4.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                        _listOfWarningCube.Add(go4);
                    }

                    break;
                case Bomb.NumberOfPlatform._4:
                    _listCubePos.Add(_originalCubePos);
                    var go5 = Instantiate(_warningCube, _originalCubePos, Quaternion.identity);
                    go5.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                    _listOfWarningCube.Add(go5);
                    _listCubePos.Add(_rightCubePos);
                    var go6 = Instantiate(_warningCube, _rightCubePos, Quaternion.identity);
                    go6.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                    _listOfWarningCube.Add(go6);
                    _listCubePos.Add(_upCubePos);
                    var go7 = Instantiate(_warningCube, _upCubePos, Quaternion.identity);
                    go7.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                    _listOfWarningCube.Add(go7);
                    _listCubePos.Add(_upRightCubePos);
                    var go8 = Instantiate(_warningCube, _upRightCubePos, Quaternion.identity);
                    go8.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
                    _listOfWarningCube.Add(go8);
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


    private bool CheckPlayer()
    {
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                if (_player.transform.position == _originalCubePos)
                {
                    return true;
                }
                return false;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    if (_player.transform.position == _originalCubePos || _player.transform.position == _rightCubePos)
                    {
                        return true;
                    }
                }
                else if (_player.transform.position == _originalCubePos || _player.transform.position == _upCubePos)
                {
                    return true;
                }
                return false;
            case Bomb.NumberOfPlatform._4:
                if (_player.transform.position == _originalCubePos || _player.transform.position == _rightCubePos || _player.transform.position == _upCubePos || _player.transform.position == _upRightCubePos)
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
            go.transform.localScale = new Vector3(grid.cellSize.x, grid.cellSize.y, 1);
        }
    }

    void OnDrawGizmos()
    {
        _originalCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2, 0);
        _rightCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2 + grid.cellSize.x, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2);
        _upCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2 + grid.cellSize.y, 0);
        _upRightCubePos = new Vector3(_moveOnX * grid.cellSize.x + grid.cellSize.x / 2 + grid.cellSize.x, _moveOnY * grid.cellSize.y + grid.cellSize.y / 2 + grid.cellSize.y, 0);

        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                Gizmos.DrawCube(_originalCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                break;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    Gizmos.DrawCube(_originalCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                    Gizmos.DrawCube(_rightCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                }
                else
                {
                    Gizmos.DrawCube(_originalCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                    Gizmos.DrawCube(_upCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                }

                break;
            case Bomb.NumberOfPlatform._4:
                Gizmos.DrawCube(_originalCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                Gizmos.DrawCube(_rightCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                Gizmos.DrawCube(_upCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                Gizmos.DrawCube(_upRightCubePos, new Vector3(grid.cellSize.x, grid.cellSize.y, 1));
                break;
            default:
                Debug.LogError("Bomb event switch enter in default");
                break;

        }
    }

 
    /* [CustomEditor(typeof(Bomb))]
    public class MyScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var myScript = target as Bomb;

            if (myScript.numberOfPlatform == Bomb.NumberOfPlatform._2)
            {
                myScript.platformAxis = (PlatformAxis)EditorGUILayout.EnumPopup("Platform Axis", myScript.platformAxis);
            }

        }
    }*/
}
