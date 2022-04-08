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

    // lever��� �Ķ���͸� ������ Trigger�� ����
    public void LeverActivate()
    {
        animator.SetTrigger("lever");
    }
}
