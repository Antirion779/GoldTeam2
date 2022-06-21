using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.UI;

public class EnemieCac : EnemieBase
{
    [Header("SPIN ME ROUND")]
    [SerializeField] private GameObject spinMe;

    [Header("Show Patern")]
    public bool showPatern;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _particleSpeed;
    [SerializeField] private int indexShowPatern = 0;
    [SerializeField] private bool isParticleMoving;
    [SerializeField] private Vector3 _nextPos;
    [SerializeField] private Button button;
    private bool isInPrePatern;
    

    protected override void OnEnable()
    {
        base.OnEnable();
        SetupOrientation(orientation);
        indexShowPatern = -1;
        //_nextPos = GetNextPaternPos(patern, 1);
    }

    private void OnDisable()
    {
        SetupOrientation(orientation);
        nextorientation = "";
    }

    protected override void Update()
    {
        base.Update();
        CheckForPlayer();

        if (showPatern)
        {
            _particle.gameObject.SetActive(true);
            ShowPatern();
        }
        else
        {
            _particle.gameObject.SetActive(false);
        }

        button.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    protected override void TurnPlayer(string _orientation)
    {
        base.TurnPlayer(_orientation);
        switch (_orientation)
        {
            case "N":
                vision[0].transform.eulerAngles = new Vector3(0, 0, 0);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = (rangeVision * 15) - 15;
                break;
            case "S":
                vision[0].transform.eulerAngles = new Vector3(0, 0, -180);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                break;
            case "E":
                spinMe.transform.eulerAngles = new Vector3(0, 0, 0);
                vision[0].transform.eulerAngles = new Vector3(0, 0, -105);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                break;
            case "W":
                spinMe.transform.eulerAngles = new Vector3(0, 180, 0);
                vision[0].transform.eulerAngles = new Vector3(0, 0, -256);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                break;
        }
    }

    protected override void MakeAMove(string[] _patern, int _paternNumber)
    {
        base.MakeAMove(_patern, _paternNumber);

        if (paternNumber == 0)
            indexShowPatern = _paternNumber -1;
        indexShowPatern++;

        isInMovement = true;
        hasPlayed = true;
    }

    protected override void SetupOrientation(visionOrientation _visionOrientation)
    {
        base.SetupOrientation(orientation);
        switch (_visionOrientation)
        {
            case visionOrientation.North:
                vision[0].transform.eulerAngles = new Vector3(0, 0, 0);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius =(rangeVision * 15) - 15;
                return;
            case visionOrientation.South:
                vision[0].transform.eulerAngles = new Vector3(0, 0, -180);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                return;
            case visionOrientation.Est:
                vision[0].transform.eulerAngles = new Vector3(0, 0, -105);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                spinMe.transform.eulerAngles = new Vector3(0, 0, 0);
                return;
            case visionOrientation.West:
                spinMe.transform.eulerAngles = new Vector3(0, 180, 0);
                vision[0].transform.eulerAngles = new Vector3(0, 0, 105);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                return;
        }
    }

    protected override void CheckForPlayer()
    {
        base.CheckForPlayer();
        RaycastHit2D hitVision = Physics2D.Raycast(transform.position, visionDir, GameManager.Instance.GetMoveDistance * rangeVision, collisionLayer);

        if (hitVision)
            vision[0].GetComponent<Light2D>().pointLightOuterRadius = hitVision.distance;

    }

    private void ShowPatern()
    {

        /*if (indexShowPatern++ < prepatern.Length && isInPrePatern && prepatern != null)
        {
            Vector3 _nextPos = GetNextPaternPos(prepatern, indexShowPatern + 1);
            if (Vector3.Distance(_particle.transform.position, _nextPos) > 0.02f && !isParticleMoving)
            {
                isParticleMoving = true;
                _particle.transform.position = Vector3.MoveTowards(_particle.transform.position, _nextPos, _particleSpeed * Time.deltaTime);
            }

            else if(Vector3.Distance(_particle.transform.position, _nextPos) > 0.02f)
            {
                isParticleMoving = false;
                indexShowPatern++;
            }
                
        }

        else if (isInPrePatern)
        {
            isInPrePatern = false;
            indexShowPatern = 0;
        }*/

        

        if (!isInPrePatern && indexShowPatern <= patern.Length - 1)
        { 
            if (Vector3.Distance(_particle.transform.localPosition, _nextPos) > 0.02f || !isParticleMoving)
            {
                isParticleMoving = true;
                _particle.transform.localPosition = Vector3.MoveTowards(_particle.transform.localPosition, _nextPos, _particleSpeed * Time.deltaTime);
            }

            else if (Vector3.Distance(_particle.transform.localPosition, _nextPos) < 0.02f && isParticleMoving)
            {
                isParticleMoving = false;
                if (indexShowPatern < patern.Length - 1)
                {
                    _nextPos = GetNextPaternPos(patern, indexShowPatern + 1);
                }
                if (indexShowPatern < patern.Length)
                {
                    indexShowPatern++;
                }
            }
        }

        else
        {
            showPatern = false;
            indexShowPatern = paternNumber - 1;
            _particle.transform.localPosition = Vector3.zero;
            _nextPos = Vector3.zero;
        }
    }

    private Vector3 GetNextPaternPos(string[] _patern, int _paternNumber)
    {
        switch (_patern[_paternNumber])
        {
            case "N":
                return new Vector3(_particle.transform.localPosition.x, _particle.transform.localPosition.y + moveDistance);
            case "S":
                return new Vector3(_particle.transform.localPosition.x, _particle.transform.localPosition.y - moveDistance);
            case "E":
                return new Vector3(_particle.transform.localPosition.x +  moveDistance, _particle.transform.localPosition.y);
            case "W":
                return new Vector3(_particle.transform.localPosition.x - moveDistance, _particle.transform.localPosition.y);
        }

        return transform.position;
    }

    public void ActivateShowPatern()
    {
        showPatern = true;
    }

    protected override void SaveAchivement()
    {
        base.SaveAchivement();

        MusicList.Instance.PlayDeathKebab();

        if(MusicManager.instance.isVibrationEnabled)
            Handheld.Vibrate();

        AchivementSaveManager.Instance.NbDieEnemyCac();
    }
}
