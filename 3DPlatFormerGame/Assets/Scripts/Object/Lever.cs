using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Lever의 손잡이가 작동하는 애니메이션 재생
    public void LeverActivate()
    {
        animator.SetTrigger("lever");
    }
}
