using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // 추적할 타겟 오브젝트의 Transform
    public float distance;      // 카메라와의 일정 거리
    public float height;        // 카메라의 높이 설정
    //public Vector3 offset = new Vector3(0, 1.0f, -1.0f);

    private Transform tr;       // 카메라 자신의 Transform 변수

    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);

        //if (Input.GetKey(KeyCode.Q))
        //{
        //    transform.RotateAround(target.position, Vector3.up, 5.0f);
        //    Debug.Log("Q");

        //    offset = transform.position - target.position;
        //    offset.Normalize();
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.RotateAround(target.position, Vector3.up, -5.0f);
        //    Debug.Log("E");

        //    offset = transform.position - target.position;
        //    offset.Normalize();
        //}
    }

    private void LateUpdate()
    {
        // 카메라 위치 설정
        tr.position = target.position - (1 * Vector3.forward * distance)
            + (Vector3.up * height);

        // 타겟을 바라보게 하기
        tr.LookAt(target);
    }
}
