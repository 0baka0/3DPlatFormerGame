using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // ������ Ÿ�� ������Ʈ�� Transform
    public float distance;      // ī�޶���� ���� �Ÿ�
    public float height;        // ī�޶��� ���� ����

    private Transform tr;       // ī�޶� �ڽ��� Transform ����

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {

        // ī�޶� ��ġ ����
        tr.position = target.position - (1 * Vector3.forward * distance)
            + (Vector3.up * height);
        // Ÿ���� �ٶ󺸰� �ϱ�
        tr.LookAt(target);
    }
}
