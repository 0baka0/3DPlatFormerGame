using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // ������ Ÿ�� ������Ʈ�� Transform
    public float distance;      // ī�޶���� ���� �Ÿ�
    public float height;        // ī�޶��� ���� ����
    public float smoothRotate;  // ����� ȸ���� ���� ����

    private Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        var currentYAngle = Mathf.LerpAngle(tr.eulerAngles.y,
            target.eulerAngles.y, smoothRotate * Time.deltaTime);

        var rot = Quaternion.Euler(0, currentYAngle, 0);

        tr.position = target.position - (rot * Vector3.forward * distance)
            + (Vector3.up * height);

        tr.LookAt(target);
    }
}
