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

    // Ư�� �±׸� ���� ������Ʈ�� ���� ���� ���� ������.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
