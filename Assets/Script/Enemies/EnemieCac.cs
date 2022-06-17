using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemieCac : EnemieBase
{
    [Header("SPIN ME ROUND")]
    [SerializeField] private GameObject spinMe;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetupOrientation(orientation);
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
                vision[0].transform.eulerAngles = new Vector3(0, 0, -105);
                vision[0].GetComponent<Light2D>().pointLightOuterRadius = rangeVision * 15;
                spinMe.transform.eulerAngles = new Vector3(0, 180, 0);
                return;
        }
    }

    protected override void SaveAchivement()
    {
        base.SaveAchivement();

        MusicList.Instance.PlayDeathKebab();

        AchivementSaveManager.Instance.NbDieEnemyCac();
    }
}
