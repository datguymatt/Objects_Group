using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOptions : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private bool isPlaying = false;
    public void ChangeVolumeSFX()
    {
        if(isPlaying == false)
        {
            audioSource.Play();
            isPlaying = true;
            StartCoroutine(Wait());
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        isPlaying = false;
    }
}
