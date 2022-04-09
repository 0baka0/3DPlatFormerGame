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

    // Spike ���ð� �Ʒ��� �������� �ִϸ��̼� ���
    public void SpikeDisabled()
    {
        animator.SetTrigger("spike");
    }
}
