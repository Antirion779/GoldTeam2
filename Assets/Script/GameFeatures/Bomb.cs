using UnityEngine;
using UnityEditor;
using System;
public class Bomb : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] GameObject _player;

    [Header("Bomb Options")]
    [SerializeField] int _actionPoint;

    [SerializeField, Range(-30, 30)] int _moveOnX = 0;
    [SerializeField, Range(-30, 30)] int _moveOnY = 0;
    public enum NumberOfPlatform { _1, _2, _4 }
    public NumberOfPlatform numberOfPlatform;
    private enum PlatformAxis { HORIZONTAL, VERTICAL }
    private PlatformAxis platformAxis;
    private int _actionRemaining;
    private bool _hasBeenUsed = false;



    private void Start()
    {
        _actionRemaining = _actionPoint;
    }
    private void OnEnable()
    {
        if (!_hasBeenUsed)
        {
            if (_actionRemaining == 0)
            {
                _hasBeenUsed = true;
                LaunchBomb();
            }
            else
                _actionRemaining--;
        }

    }

    private void LaunchBomb()
    {
        //Put Anim
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                if (_player.transform.position == new Vector3(_moveOnX, _moveOnY, 0))
                {
                    //Kill player
                }
                break;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    if (_player.transform.position == new Vector3(_moveOnX, _moveOnY, 0) || _player.transform.position == new Vector3(_moveOnX + 1, _moveOnY, 0))
                    {
                        //Kill player
                    }
                }
                else if (_player.transform.position == new Vector3(_moveOnX, _moveOnY, 0) || _player.transform.position == new Vector3(_moveOnX, _moveOnY + 1, 0))
                {
                    //Kill player
                }
                break;
            case Bomb.NumberOfPlatform._4:
                if (_player.transform.position == new Vector3(_moveOnX, _moveOnY, 0) || _player.transform.position == new Vector3(_moveOnX + 1, _moveOnY, 0) || _player.transform.position == new Vector3(_moveOnX + 1, _moveOnY + 1, 0) || _player.transform.position == new Vector3(_moveOnX, _moveOnY + 1, 0))
                {
                    //Kill player
                }
                break;
            default:
                Debug.LogError("Bomb event switch enter in default");
                break;

        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        switch (numberOfPlatform)
        {
            case Bomb.NumberOfPlatform._1:
                Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY, 0), new Vector3(1, 1, 1));
                break;
            case Bomb.NumberOfPlatform._2:
                if (platformAxis == Bomb.PlatformAxis.HORIZONTAL)
                {
                    Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY, 0), new Vector3(1, 1, 1));
                    Gizmos.DrawCube(new Vector3(_moveOnX + 1, _moveOnY, 0), new Vector3(1, 1, 1));
                }
                else
                {
                    Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY, 0), new Vector3(1, 1, 1));
                    Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY + 1, 0), new Vector3(1, 1, 1));
                }

                break;
            case Bomb.NumberOfPlatform._4:
                Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY, 0), new Vector3(1, 1, 1));
                Gizmos.DrawCube(new Vector3(_moveOnX + 1, _moveOnY, 0), new Vector3(1, 1, 1));
                Gizmos.DrawCube(new Vector3(_moveOnX, _moveOnY + 1, 0), new Vector3(1, 1, 1));
                Gizmos.DrawCube(new Vector3(_moveOnX + 1, _moveOnY + 1, 0), new Vector3(1, 1, 1));
                break;
            default:
                Debug.LogError("Bomb event switch enter in default");
                break;

        }
    }

    [CustomEditor(typeof(Bomb))]
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
    }
}
