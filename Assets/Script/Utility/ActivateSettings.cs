using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject levelMenu;

    private bool isActivate;

    private void Awake()
    {
        settingsMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

    public void ActivateSettingsMenu()
    {
        if (isActivate)
        {
            isActivate = false;
            settingsMenu.SetActive(false);
            levelMenu.SetActive(true);
        }
        else
        {
            settingsMenu.SetActive(true);
            isActivate = true;
            levelMenu.SetActive(false);
            MusicManager.instance.SetAnimation();
        }
    }

}
