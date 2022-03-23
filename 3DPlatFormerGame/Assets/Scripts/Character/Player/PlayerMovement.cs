using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5;    // 이동 속도
    private Vector3 moveForce;      // 이동 힘 (x, z와 y축을 별도로 계산해 실제 이동에 적용)

    public float rotationSpeed;     // 회전 속도

    private CharacterController characterController;    // 플레이어 이동 제어를 위한 컴포넌트

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = Mathf.Max(0, value);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 초당 moveForce 속력으로 이동
        characterController.Move(moveForce * Time.deltaTime);
    }

    // 위나 아래를 바라보고 이동할 경우 캐릭터가 공중으로 뜨거나 아래로 가라앉으려 하기 때문에
    // direction을 그대로 사용하지 않고, moveForce변수에 x, z값만 넣어서 사용
    // 카메라 회전으로 전방 방향이 변하기 때문에
    // 회전 값을 곱해서 연산해야 한다
    public void MoveTo(Vector3 direction)
    {
        // 이동 방향 = 캐릭터의 회전 값 * 방향 값
        direction = new Vector3(direction.x, 0, direction.z);
        
        // 부드러운 움직임
        if (direction.sqrMagnitude > .01f)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));

            transform.LookAt(transform.position + forward);
        }

        // 이동 힘 = 이동방향 * 속도
        moveForce = new Vector3(direction.x * moveSpeed, moveForce.y, direction.z * moveSpeed);
    }
}
