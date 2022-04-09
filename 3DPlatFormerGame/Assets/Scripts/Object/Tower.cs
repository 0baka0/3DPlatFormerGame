using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Tower�� ���� ������ �ִϸ��̼� ���
    public void OpenDoor()
    {
        animator.SetTrigger("open");
    }
}
