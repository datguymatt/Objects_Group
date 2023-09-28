using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    // menu gameobject assigned
    public GameObject MainMenuUI, OptionsMenuUI;

    // this is where music audiomixer is assigned 
    public AudioMixer mainmixer;
    
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
        mainmixer.SetFloat("volume1", volume);
        mainmixer.SetFloat("volume2", volume);
        mainmixer.SetFloat("volume3", volume);
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
