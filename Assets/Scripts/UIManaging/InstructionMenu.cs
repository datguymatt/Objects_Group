using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionMenu : MonoBehaviour
{
    [SerializeField] private int instructionTimer;

    private void Start()
    {
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(instructionTimer);
        this.gameObject.SetActive(false);
    }
}
