using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Wall 이라는 태그를 가진 Collider에 닿았을 때 삭제
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            // 대포알 삭제
            Destroy(gameObject);
        }
    }
}
