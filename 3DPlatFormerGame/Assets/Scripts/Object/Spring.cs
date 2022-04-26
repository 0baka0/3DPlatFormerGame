using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // �������� ����� �� ��Ÿ���� �ִϸ��̼�
    public void PutSpring()
    {
        // ������ �ִϸ��̼� ���
        animator.SetBool("spring", true);
    }

    // ������ �ִϸ��̼��� ������ ���ư��� ����
    public void RestorationSpring()
    {
        // ������ �ִϸ��̼� ��� ��, �����·� ����
        animator.SetBool("spring", false);
    }
}
