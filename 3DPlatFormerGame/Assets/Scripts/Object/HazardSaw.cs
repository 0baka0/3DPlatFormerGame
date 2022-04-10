using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSaw : MonoBehaviour
{
    public float rightMax;  // �·� �̵������� (x)�ִ�
    public float leftMax;   // ��� �̵������� (x)�ִ�
    public float y;         // Y��
    public float z;         // Z��
    float currentPosition;  // ���� ��ġ(x) ����
    public float direction; // �̵��ӵ� + ����

    private void Start()
    {
        currentPosition = transform.position.x;
    }

    private void Update()
    {
        currentPosition += direction * Time.deltaTime;
        // ���� ��ġ�� ��� �̵� ������ (x)�ִ񰪺��� ũ�ų� ���ٸ�
        // direction�� -1�� ���� ������ ���ְ� ���� ��ġ�� ��� �̵������� (x)�ִ������ ����
        if(currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
        }
        // ���� ��ġ�� �·� �̵������� (x)�ִ񰪺��� ũ�ų� ���ٸ�
        // direction�� -1�� ���� ������ ���ְ� ���� ��ġ�� �·� �̵������� (x)�ִ밪���� ����
        else if(currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
        }
        // HazardSaw�� ��ġ�� ���� ���� ��ġ�� ó��
        transform.position = new Vector3(currentPosition, y, z);
    }
}
