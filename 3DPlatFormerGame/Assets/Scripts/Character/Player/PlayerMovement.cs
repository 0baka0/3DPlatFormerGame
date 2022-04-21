using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;    // �̵� �ӵ�
    private Vector3 moveForce;      // �̵� �� (x, z�� y���� ������ ����� ���� �̵��� ����)

    public float jumpForce;         // ���� ��
    public float gravity;           // �߷� ���
    public float spring = 2f;

    public float rotationSpeed;     // ȸ�� �ӵ�

    // �÷��̾� �̵� ��� ���� ������Ʈ
    private CharacterController characterController;    

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0, value); // value ���� ��ȯ
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // ����� �������� �߷¸�ŭ y�� �̵��ӵ� ����
        if(!characterController.isGrounded)
        {
            moveForce.y += gravity * Time.deltaTime;
        }

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
        direction = new Vector3(direction.x, 0, direction.z);
        
        // �ε巯�� ������
        if (direction.sqrMagnitude > .01f)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            // ���� �Ĵٺ���.
            transform.LookAt(transform.position + forward);
        }

        // �̵� �� = �̵����� * �ӵ�
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }

    // ����
    public void Jump()
    {
        // ���� ����� ��
        if(characterController.isGrounded)
        {
            // y������ jumpForce�� ����ŭ �����Ѵ�
            // ���� �ö󰣴�
            moveForce.y = jumpForce;
        }
    }

    public void Spring()
    {
        moveForce.y = jumpForce * spring;
    }
}
