using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSpikeTrap : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // spike라는 파라미터를 가지는 Trigger를 실행
    public void SpikeDisabled()
    {
        animator.SetTrigger("spike");
    }
}
