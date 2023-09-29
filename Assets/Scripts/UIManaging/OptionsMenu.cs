using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    // menu gameobject assigned
    public GameObject MainMenuUI, OptionsMenuUI;

    public int fadeVolumeRate;

    // this is where music audiomixer is assigned 
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    // this is where sound audiomixer is assigned
    // public AudioMixer soundMixer;
    public void BackToMainMenu()
    {
        MainMenuUI.SetActive(true);
        OptionsMenuUI.SetActive(false);
    }
    // fullscreen check mark  script
    public void FullScreenCheck(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    // This is where Music is operated using Slider where -80 lowest and 0 is the highest value
    public void MusicVolumeSlider(float volume)
    {
        // This operates the exact mixer you choice
        //fade out made more smooth by the fadeVolumeRate
        if (fadeVolumeRate != 0)
        {
            musicMixer.SetFloat("volume1", (volume / fadeVolumeRate));
            if(volume < -75)
            {
                musicMixer.SetFloat("volume1", -80);

            }
        }
        
        //mainmixer.SetFloat("volume2", volume);
        //mainmixer.SetFloat("volume3", volume);
    }
    public void SFXVolumeSlider(float volume)
    {
        // This operates the exact mixer you choice
        //fade out made more smooth by the fadeVolumeRate
        if (fadeVolumeRate != 0)
        {
            sfxMixer.SetFloat("volume1", (volume / fadeVolumeRate));
            if (volume < -75)
            {
                sfxMixer.SetFloat("volume1", -80);
            }
        }

        //mainmixer.SetFloat("volume2", volume);
        //mainmixer.SetFloat("volume3", volume);
    }
    // This is where Sound is operated using Slider where -80 lowest and 0 is the highest value
    //public void SoundVolumeSlider(float volume)
    //{
    //    // This operates the exact mixer you choice 
    //   soundMixer.SetFloat("volume", volume);
    //}
    // quality dropdown script
    public void SetQualityDropBox(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
