using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;

    public float MoveSpeed
    {
        // Animator View�� �ִ� float ���� ���� "ParamName"�� ���� ��ȯ
        get => animator.GetFloat("movementSpeed");
        // Animator View�� �ִ� float Ÿ�� ���� "ParamName"�� ���� value�� ����
        set => animator.SetFloat("movementSpeed", value);
    }

    private void Awake()
    {
        // "Player" ������Ʈ �������� �ڽ� ������Ʈ��
        // "PlayerBody" ������Ʈ�� Animator ������Ʈ�� �ִ�
        animator = GetComponentInChildren<Animator>();
    }

    public void PlayTrigger(string stateName)
    {
        animator.SetTrigger(stateName);
    }

    public void PlayBool(string stateName, bool param)
    {
        animator.SetBool(stateName, param);
    }
}
