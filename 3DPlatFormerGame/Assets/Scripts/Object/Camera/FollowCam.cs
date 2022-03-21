using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // 추적할 타겟 오브젝트의 Transform
    public float distance;      // 카메라와의 일정 거리
    public float height;        // 카메라의 높이 설정
    public float smoothRotate;  // 부즈러운 회전을 위한 변수

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
