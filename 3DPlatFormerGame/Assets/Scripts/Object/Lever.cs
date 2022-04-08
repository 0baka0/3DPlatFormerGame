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

    // lever라는 파라미터를 가지는 Trigger를 실행
    public void LeverActivate()
    {
        animator.SetTrigger("lever");
    }
}
