using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Wall �̶�� �±׸� ���� Collider�� ����� �� ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            // ������ ����
            Destroy(gameObject);
        }
    }
}
