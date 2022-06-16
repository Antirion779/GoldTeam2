using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSettings : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject levelMenu;
    public Animator animSettings;

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
            animSettings.SetBool("Actif", false);
            StartCoroutine(ExitMenuTime());
        }
        else
        {
            StopCoroutine(ExitMenuTime());
            settingsMenu.SetActive(true);
            isActivate = true;
            levelMenu.SetActive(false);
            MusicManager.instance.SetAnimation();
            animSettings.SetBool("Actif", true);
        }
    }

    private IEnumerator ExitMenuTime()
    {
        yield return new WaitForSeconds(0.5f);
        settingsMenu.SetActive(false);
        levelMenu.SetActive(true);
    }

}
