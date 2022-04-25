using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PutSpring()
    {
        animator.SetBool("spring", true);
    }

    public void RestorationSpring()
    {
        animator.SetBool("spring", false);
    }

    // 특정 태그를 가진 오브젝트의 힘을 위로 세게 보낸다.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
