using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance = null;
    public static MusicManager instance { get => _instance; }

    [Header("Component")] 
    [SerializeField] private Button musiqueButton;
    [SerializeField] private Animator musiqueButtonAnim;
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Animator soundEffectButtonAnim;
    [SerializeField] private Button vibrationButton;
    [SerializeField] private Animator vibrationButtonAnim;

    [Header("Option")]
    [SerializeField] private bool _isSoundEnabled = true;
    public bool isSoundEnable
    {
        get { return _isSoundEnabled; }
        set { _isSoundEnabled = value; }
    }
    [SerializeField] private bool _isVibrationEnabled = true;
    public bool isVibrationEnabled
    {
        get { return _isVibrationEnabled; }
        set { _isVibrationEnabled = value; }
    }

    [SerializeField] private bool _isSoundEffectEnabled = true;

    public bool isSoundEffectEnabled
    {
        get { return _isSoundEffectEnabled; }
        set { _isSoundEffectEnabled = value; }
    }

    public bool _isInMainMenu;
    public bool isInMainMenu
    {
        get { return _isInMainMenu; }
        set { _isInMainMenu = value; }
    }


    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        isVibrationEnabled = PlayerPrefs.GetInt("isVibrationEnabled") == 1;
        SwitchVibration();
        isSoundEnable = PlayerPrefs.GetInt("isSoundEnable") == 1;
        SwitchSound();
        isSoundEffectEnabled = PlayerPrefs.GetInt("isSoundEffectEnabled") == 1;
        SwitchSoundEffect();
    }

    public void SetAnimation()
    {
        vibrationButtonAnim.SetBool("Vib", isVibrationEnabled);
        musiqueButtonAnim.SetBool("Music", isSoundEnable);
        soundEffectButtonAnim.SetBool("FX", isSoundEffectEnabled);
    }

    public void SwitchSoundEffect()
    {
        if (isSoundEffectEnabled)
        {
            //enable sound
            isSoundEffectEnabled = false;
            PlayerPrefs.SetInt("isSoundEffectEnabled", isSoundEffectEnabled ? 0 : 1);
            soundEffectButtonAnim.SetBool("FX", false);
        }
        else
        {
            //disable sound
            isSoundEffectEnabled = true; 
            PlayerPrefs.SetInt("isSoundEffectEnabled", isSoundEffectEnabled ? 0 : 1);
            soundEffectButtonAnim.SetBool("FX", true);
        }
    }
    public void SwitchSound()
    {
        if (isSoundEnable)
        {
            //disable sound
            isSoundEnable = false;
            PlayerPrefs.SetInt("isSoundEnable", isSoundEnable ? 0 : 1);
            musiqueButtonAnim.SetBool("Music", false);
        }
        else
        {
            //enable sound
            isSoundEnable = true;
            PlayerPrefs.SetInt("isSoundEnable", isSoundEnable ? 0 : 1);
            musiqueButtonAnim.SetBool("Music", true);
        }
    }

    public void SwitchVibration()
    {
        if (isVibrationEnabled)
        {
            //enable Vibration
            isVibrationEnabled = false;
            PlayerPrefs.SetInt("isVibrationEnabled", isVibrationEnabled ? 0 : 1);
            vibrationButtonAnim.SetBool("Vib", false);
        }
        else
        {
            //disable Vibration
            isVibrationEnabled = true;
            PlayerPrefs.SetInt("isVibrationEnabled", isVibrationEnabled?0:1);
            vibrationButtonAnim.SetBool("Vib", true);
            Handheld.Vibrate();
        }
    }


}
