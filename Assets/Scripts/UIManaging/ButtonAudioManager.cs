using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioManager : MonoBehaviour
{
    public AudioSource buttonFx;
    public AudioClip hoverFx;
    public AudioClip clickFx;
    

    public void HoverSound()
    {
        buttonFx.PlayOneShot(hoverFx);
    }
    public void ClickSound()
    {
        buttonFx.PlayOneShot(clickFx);
    }
}
