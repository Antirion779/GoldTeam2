using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicList : MonoBehaviour
{
    public static MusicList Instance;
    [Header("SFX")]
    [SerializeField] private AudioClip closeDoor;
    [SerializeField] private AudioClip openDoor, Oil, deathKebab, deathSneeper, patientHeal, finalDoorOpen, takePills, groundBreak, endLevel, playerMovement, bombExplosion;
    
    [Header("Music")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("AudioSource")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    private bool isMusicPlay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (MusicManager.instance.isSoundEnable && !isMusicPlay)
        {
            if (MusicManager.instance.isInMainMenu)
            {
                musicAudioSource.clip = menuMusic;
            }
            else
            {
                musicAudioSource.clip = gameMusic;
            }
            musicAudioSource.Play();
            isMusicPlay = true;
        }
        else if (!MusicManager.instance.isSoundEnable)
        {
            musicAudioSource.clip = null;
            isMusicPlay = false;
        }
    }

    public void PlayCloseDoor()
    {
        PlaySFXSong(closeDoor);
    }

    public void PlayOpenDoor()
    {
        PlaySFXSong(openDoor);
    }

    public void PlayOil()
    {
        PlaySFXSong(Oil);
    }

    public void PlayDeathKebab()
    {
        PlaySFXSong(deathKebab);
    }

    public void PlayDeathSneeper()
    {
        PlaySFXSong(deathSneeper);
    }

    public void PlayPatientHeal()
    {
        PlaySFXSong(patientHeal);
    }

    public void PlayFinalDoorOpen()
    {
        PlaySFXSong(finalDoorOpen);
    }

    public void PlayTakePills()
    {
        PlaySFXSong(takePills);
    }

    public void PlayGroundBreak()
    {
        PlaySFXSong(groundBreak);
    }

    public void PlayEndLevel()
    {
        PlaySFXSong(endLevel);
    }

    public void PlayPlayerMovement()
    {
        PlaySFXSong(playerMovement);
    }

    public void PlayBombExplosion()
    {
        PlaySFXSong(bombExplosion);
    }

    private void PlaySFXSong(AudioClip sfx)
    {
        if (MusicManager.instance.isSoundEffectEnabled)
        {
            sfxAudioSource.clip = sfx;
            sfxAudioSource.Play();
        }
    }

    public void StopOilSong()
    {
        if (sfxAudioSource.clip == Oil)
        {
            sfxAudioSource.Stop();
        }
    }
}
