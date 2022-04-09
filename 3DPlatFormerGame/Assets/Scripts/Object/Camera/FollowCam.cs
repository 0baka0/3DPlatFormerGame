using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;    // ������ Ÿ�� ������Ʈ�� Transform
    public float distance;      // ī�޶���� ���� �Ÿ�
    public float height;        // ī�޶��� ���� ����
    //public Vector3 offset = new Vector3(0, 1.0f, -1.0f);

    private Transform tr;       // ī�޶� �ڽ��� Transform ����

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
        // ī�޶� ��ġ ����
        tr.position = target.position - (1 * Vector3.forward * distance)
            + (Vector3.up * height);

        // Ÿ���� �ٶ󺸰� �ϱ�
        tr.LookAt(target);
    }
}
