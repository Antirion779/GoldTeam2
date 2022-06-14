using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousGameEffect : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _particleSpeed;

    private int _waypointIndex = 1;
    public bool _canStartEffect;

    private void Awake()
    {
        if(_particle != null)
            _particle.Stop();
    }

    public bool SameLevel()
    {
        if (PlayerPosManager.Instance._currentLevel != SceneManager.GetActiveScene().name)
        {
            PlayerPosManager.Instance._currentLevel = SceneManager.GetActiveScene().name;
            return false;
        }
        return true;
    }
    private void StartEffect()
    {
        
        if (PlayerPosManager.Instance.ListPreviousPlayerPos.Count > 0 && _particle != null)
        {
            _particle.transform.position = PlayerPosManager.Instance.ListPreviousPlayerPos[0];
            _canStartEffect = true;
            _particle.Play();
        }
    }
    private void StopEffect()
    {
        if (_particle != null)
        {
            _particle.Stop();
            _canStartEffect = false;
        }
    }

    private void Update()
    {
        if (_canStartEffect && _particle != null)
            ShowPreviousMoove();
    }

    private void ShowPreviousMoove()
    {

        if (_waypointIndex < PlayerPosManager.Instance.ListPreviousPlayerPos.Count)
        {
            if (Vector3.Distance(_particle.transform.position, PlayerPosManager.Instance.ListPreviousPlayerPos[_waypointIndex]) > 0.02f)
            {
                _particle.transform.position = Vector3.MoveTowards(_particle.transform.position, PlayerPosManager.Instance.ListPreviousPlayerPos[_waypointIndex], _particleSpeed * Time.deltaTime);
            }
            else
            {
                _waypointIndex++;
            }

        }
        else
        {
            StartEffect();
        }
    }

    public void CheckPlayerMoove(Vector2 previousPos) //Check if player moove is the same as last game
    {
        bool _isSamePos = false;
        foreach(Vector2 vec in PlayerPosManager.Instance.ListPreviousPlayerPos)
        {
            if(vec == previousPos)
                _isSamePos = true;
        }

        if(!_isSamePos)
        {
            StopEffect();
        }
    }
}
