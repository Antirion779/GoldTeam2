using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemieRange : EnemieBase
{
    [SerializeField][Tooltip("N -> E -> S -> W or it's gonna be fuck up")] private GameObject[] sneepeurs;
    private string[] sneepeurDir = new string[4] {"N","E","S","W"};
    private int sneepeurDirId;

    protected override void OnEnable()
    {
        base.OnEnable();

        foreach (GameObject visions in vision)
            visions.GetComponent<Light2D>().pointLightOuterRadius = (rangeVision * 10) + 8;

        SetupOrientation(orientation);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(canShoot)
            CheckForPlayer();
    }

    protected override void MakeAMove(string[] _patern, int _paternNumber)
    {
        base.MakeAMove(_patern, _paternNumber);
        vision[sneepeurDirId].SetActive(false);

        switch (_patern[_paternNumber])
        {
            case "TR":
                if (sneepeurDirId - 1 >= 0)
                    sneepeurDirId--;
                else
                    sneepeurDirId = sneepeurDir.Length - 1;
                break;
            case "TL":
                if (sneepeurDirId + 1 < sneepeurDir.Length)
                    sneepeurDirId++;
                else
                    sneepeurDirId = 0;
                break;

            case "A":
                vision[sneepeurDirId].SetActive(true);
                canShoot = true;
                break;
        }
    }

    protected override void TurnPlayer(string _orientation)
    {
        ResetSprite();
        if (_orientation == null)
            _orientation = sneepeurDir[sneepeurDirId];

        base.TurnPlayer(_orientation);
        switch (_orientation)
        {
            case "N":
                sneepeurs[0].SetActive(true);
                sneepeurDirId = 0;
                break;
            case "S":
                sneepeurs[2].SetActive(true);
                sneepeurDirId = 2;
                break;
            case "E":
                sneepeurs[1].SetActive(true);
                sneepeurDirId = 1;
                break;
            case "W":
                sneepeurs[3].SetActive(true);
                sneepeurDirId = 3;
                break;
        }
    }

    protected override void SetupOrientation(visionOrientation _visionOrientation)
    {
        base.SetupOrientation(orientation);
        ResetSprite();
        switch (_visionOrientation)
        {
            case visionOrientation.North:
                sneepeurs[0].SetActive(true);
                sneepeurDirId = 0;
                return;
            case visionOrientation.South:
                sneepeurs[2].SetActive(true);
                sneepeurDirId = 2;
                return;
            case visionOrientation.Est:
                sneepeurs[1].SetActive(true);
                sneepeurDirId = 1;
                return;
            case visionOrientation.West:
                sneepeurs[3].SetActive(true);
                sneepeurDirId = 3;
                return;
        }
    }

    private void ResetSprite()
    {
        foreach (var sneepeur in sneepeurs)
        {
            sneepeur.SetActive(false);
        }
    }
}
