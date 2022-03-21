using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;     // �̵� �ӵ�
    private Vector3 moveForce;  // �̵� �� (x, z�� y���� ������ ����� ���� �̵��� ����)

    private CharacterController characterController;    // �÷��̾� �̵� ��� ���� ������Ʈ

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // �ʴ� moveForce �ӷ����� �̵�
        characterController.Move(moveForce * Time.deltaTime);
    }

    // ���� �Ʒ��� �ٶ󺸰� �̵��� ��� ĳ���Ͱ� �������� �߰ų� �Ʒ��� ��������� �ϱ� ������
    // direction�� �״�� ������� �ʰ�, moveForce������ x, z���� �־ ���
    // ī�޶� ȸ������ ���� ������ ���ϱ� ������
    // ȸ�� ���� ���ؼ� �����ؾ� �Ѵ�
    public void MoveTo(Vector3 direction)
    {
        // �̵� ���� = ĳ������ ȸ�� �� * ���� ��
        direction = transform.rotation * new Vector3(direction.x, 0, direction.z);

        // �̵� �� = �̵����� * �ӵ�
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }
}
