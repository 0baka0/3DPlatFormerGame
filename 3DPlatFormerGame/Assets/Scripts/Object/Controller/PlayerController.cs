using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    private KeyCode keyCodeRun1P = KeyCode.LeftShift;   // �޸��� Ű 1P
    private KeyCode keyCodeRun2P = KeyCode.RightShift;  // �޸��� Ű 2P
    private KeyCode keyCodeJump1P = KeyCode.V;          // ���� Ű 1P
    private KeyCode keyCodeJump2P = KeyCode.Space;      // ���� Ű 2P

    [Header("Audio Clips Afterwards Change")]
    public AudioClip audioClipWalk;     // �ȱ� ����
    public AudioClip audioClipRun;      // �޸��� ����

    private PlayerMovement movement;    // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;              // �̵��ӵ� ���� �÷��̾� ����
    private PlayerAnim playerAnim;      // �ִϸ��̼� ��� ����
    private AudioSource audioSource;    // ���� ��� ����

    public bool player2P;               // 1P�� 2P�� ����

    private void Awake()
    {
        // ���콺 Ŀ���� ������ �ʰ� �����ϰ�, ���� ��ġ�� ������Ų��
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        movement = GetComponent<PlayerMovement>();
        status = GetComponent<Status>();
        playerAnim = GetComponent<PlayerAnim>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateMove();
        UpdateJump();
    }

    // 1P�� 2P �̵�
    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");       // aŰ = ���� �̵�, dŰ ������ �̵�
        float z = Input.GetAxisRaw("Vertical");         // wŰ = ���� �̵�, sŰ �Ʒ��� �̵�

        float x2 = Input.GetAxisRaw("Horizontal2P");    // lŰ = ���� �̵�, 'Ű ������ �̵�
        float z2 = Input.GetAxisRaw("Vertical2P");      // pŰ = ���� �̵�, ;Ű �Ʒ��� �̵�

        // Player 1
        if (player2P == false)
        {
            // �̵����� �� (�ȱ� or �ٱ�)
            if (x != 0 || z != 0)
            {
                // �޸��� ������ Ȯ���ϴ� bool ��
                bool isRun = false;

                // keyCodeRun(LeftShift)�� ������ �� �޸���
                isRun = Input.GetKey(keyCodeRun1P);

                // ���� ����Ʈ�� ���ȴٸ� �޸���, �װ� �ƴ϶�� �ȱ�
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // ����Ű �Է� ���δ� �� ������ Ȯ���ϱ� ������
                // ������� ���� �ٽ� ������� �ʵ��� isPlaying���� üũ�ؼ� ���
                if (audioSource.isPlaying == false)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            // ���ڸ��� �������� ��
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // ������ �� ���尡 ������̸� ����
                audioSource.Stop();
            }

            movement.MoveTo(new Vector3(x, 0, z));
        }

        // Player 2
        else if (player2P == true)
        {
            // �̵����� �� (�ȱ� or �ٱ�)
            if (x2 != 0 || z2 != 0)
            {
                // �޸��� ������ Ȯ���ϴ� bool ��
                bool isRun = false;

                // keyCodeRun2P(RightShift)�� ������ �� �޸���
                isRun = Input.GetKey(keyCodeRun2P);

                // ������ ����Ʈ�� ���ȴٸ� �޸���, �װ� �ƴ϶�� �ȱ�
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // ����Ű �Է� ���δ� �� ������ Ȯ���ϱ� ������
                // ������� ���� �ٽ� ������� �ʵ��� isPlaying���� üũ�ؼ� ���
                if (audioSource.isPlaying == false)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            // ���ڸ��� �������� ��
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // ������ �� ���尡 ������̸� ����
                audioSource.Stop();
            }

            movement.MoveTo(new Vector3(x2, 0, z2));
        }
    }

    // 1P�� 2P ����
    private void UpdateJump()
    {
        if (player2P == false)
        {
            if (Input.GetKeyDown(keyCodeJump1P))
            {
                Debug.Log("����");
                movement.Jump();
                playerAnim.PlayTrigger("doJump");
                playerAnim.PlayBool("isJump", true);
            }
            else
            {
                playerAnim.PlayBool("isJump", false);
            }    
        }

        else if (player2P == true)
        {
            if (Input.GetKeyDown(keyCodeJump2P))
            {
                Debug.Log("����2");
                movement.Jump();
            }

        }
    }

}
