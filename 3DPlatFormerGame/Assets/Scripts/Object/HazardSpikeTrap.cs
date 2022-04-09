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

    // Spike 가시가 아래로 내려가는 애니메이션 재생
    public void SpikeDisabled()
    {
        animator.SetTrigger("spike");
    }
}
