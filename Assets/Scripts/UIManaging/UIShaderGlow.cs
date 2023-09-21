using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShaderGlow : MonoBehaviour
{
    [SerializeField]
    private int count;
    private bool timeToSub = false;
    private bool loop = true;
    private int loopCount;
    private float speed = 0.0001f;
    private float negativeSpeed = -0.0001f;
    [SerializeField] private float speedRate = 0.01f;


    [SerializeField] private Material colorDissolve;
    private float lerpColorVal;

    void Awake()
    {
        
        StartCoroutine(Animate());

    }

    IEnumerator Animate()
    {
        while (loop == true)
        {
            //add
            while (timeToSub == false)
            {
                yield return new WaitForSeconds(0.01f);
                if (colorDissolve.GetFloat("_GlowOuter") != 0.9f)
                {
                    speed = speed + speedRate;
                    lerpColorVal = Mathf.PingPong(speed, 1);
                    colorDissolve.SetFloat("_GlowOuter", lerpColorVal);
                }
                if (colorDissolve.GetFloat("_GlowOuter") == 0.9f)
                {
                    timeToSub = true;
                    break;
                }


            }
            //substract values
            while (timeToSub == true && colorDissolve.GetFloat("_GlowOuter") != 0.1f)
            {
                yield return new WaitForSeconds(0.01f);
                lerpColorVal = Mathf.PingPong(negativeSpeed, 1);
                colorDissolve.SetFloat("_GlowOuter", lerpColorVal);
                count--;

            }
            timeToSub = false;
        }
    }
}
