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

    // 스프링을 밟았을 때 나타나는 애니메이션
    public void PutSpring()
    {
        // 스프링 애니메이션 재생
        animator.SetBool("spring", true);
    }

    // 스프링 애니메이션이 끝나고 돌아가는 상태
    public void RestorationSpring()
    {
        // 스프링 애니메이션 재생 후, 원상태로 복구
        animator.SetBool("spring", false);
    }
}
