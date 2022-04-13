 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSaw : MonoBehaviour
{
    Vector3 pos; // ������ġ
    public float delta; // ��(��)�� �̵������� (x)�ִ밪
    public float speed; // �̵��ӵ�

    void Start()
    {
        // pos�� ���� ��ġ�� ����
        pos = transform.position;
    }

    void Update()
    {
        // ���� ��ġ�� ����
        Vector3 v = pos;

        // ������
        v.x += delta * Mathf.Sin(Time.time * speed);

        // ������ġ�� ���� v���� ����
        transform.position = v;
    }
}
