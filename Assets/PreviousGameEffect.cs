using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousGameEffect : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject _lightedCube;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _particleSpeed;

    private int _waypointIndex = 1;
    public bool _canStartEffect;
    private List<GameObject> ListLightedCube = new List<GameObject> ();

    private void Awake()
    {
        _particle.Stop();
        ShowLightedGrid();
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

    private void ShowLightedGrid()
    {
        if(PlayerPosManager.Instance.ListPreviousPlayerPos.Count > 0)
        {
            foreach (Vector2 vec in PlayerPosManager.Instance.ListPreviousPlayerPos)
            {
                var go = Instantiate(_lightedCube, new Vector3(vec.x, vec.y, -1), Quaternion.identity);
                go.transform.localScale *= grid.cellSize.x;
                ListLightedCube.Add(go);
            }
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
            DestroyLightedCube();
            StopEffect();
        }
    }

    public void DestroyLightedCube()
    {
        foreach (GameObject go in ListLightedCube)
        {
            Destroy(go);
        }

        ListLightedCube.Clear();
    }
}
