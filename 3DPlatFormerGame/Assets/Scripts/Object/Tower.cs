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

    // Tower의 문이 열리는 애니메이션 재생
    public void OpenDoor()
    {
        animator.SetTrigger("open");
    }
}
