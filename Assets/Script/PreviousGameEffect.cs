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

    private string _currentLevel;

    private void Awake()
    {
        _particle.Stop();
        if(_currentLevel != SceneManager.GetActiveScene().name)
        {
            PlayerPosManager.Instance.ListCurrentPlayerPos.Clear();
            _currentLevel = SceneManager.GetActiveScene().name;
        }

    }

    private void Start()
    {
        
    }
    private void StartEffect()
    {
        
        if (PlayerPosManager.Instance.ListPreviousPlayerPos.Count > 0)
        {
            _particle.transform.position = PlayerPosManager.Instance.ListPreviousPlayerPos[0];
            _canStartEffect = true;
            _particle.Play();
        }
    }
    private void StopEffect()
    {
        _particle.Stop();
        _canStartEffect = false;
    }

    private void Update()
    {
        if (_canStartEffect)
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
            StopEffect();
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
