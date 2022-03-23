using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;

    public float MoveSpeed
    {
        // Animator View에 있는 float 탕비 변수 "ParamName"의 값을 반환
        get => animator.GetFloat("movementSpeed");
        // Animator View에 있는 float 타입 변수 "ParamName"의 값을 value로 설정
        set => animator.SetFloat("movementSpeed", value);
    }

    private void Awake()
    {
        // "Player" 오브젝트 기준으로 자식 오브젝트인
        // "PlayerBody" 오브젝트에 Animator 컴포넌트가 있다
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
