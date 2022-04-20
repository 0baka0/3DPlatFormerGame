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
    public AudioClip audioClipWalk;         // �ȱ� ����
    public AudioClip audioClipRun;          // �޸��� ����
    public AudioClip audioClipJump;         // ���� ����

    private PlayerMovement movement;        // Ű���� �Է����� �÷��̾� �̵�, ����
    private Status status;                  // �̵��ӵ� ���� �÷��̾� ����
    private PlayerAnim playerAnim;          // �ִϸ��̼� ��� ����
    private AudioSource audioSource;        // ���� ��� ����
    public Lever lever;                     // Lever ����
    public Lever lever2;                    // Lever2 ����
    public Lever lever3;                    // Lever3 ����
    public HazardSpikeTrap hazardSpikeTrap; // HazardSpikeTrap ����
    public GameObject spikyBallCollection;  // SpikyBall ���� ������ �ִ� ������Ʈ
    public TransparencyGround transparency; // �ȿ� �ִ� Ground���� ����
    public Tower tower;                     // Tower ����

    public bool isJump;                     // ���� ���� ����
    public bool player2P;                   // 1P�� 2P�� ����

    public GameObject fallingTarget;        // ���� ���� �� ������ �� TargetPos
    public GameObject bridgeTarget;         // ȭ��ǥ��� ���� �ʾ��� �� �������� TargetPos
    public GameObject bridgeTarget2;        // ȭ��ǥ��� ���� �ʾ��� �� �������� TargetPos
    public GameObject fallingStage1Target;  // Stage1���� ���縦 �ϰų� Spike �±׸� ���� ������Ʈ�� ����� �� ������ �� TargetPos
    public GameObject fallingStage2Target;  // Stage2���� ���縦 �ϰų� ������ �� TargetPos

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
                // �׿� ���� �ӵ� ����, �ִϸ��̼� ���, ���� ���
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                //audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // ����Ű �Է� ���δ� �� ������ Ȯ���ϱ� ������
                // ������� ���� �ٽ� ������� �ʵ��� isPlaying���� üũ�ؼ� ���
                //if (audioSource.isPlaying == false)
                //{
                //    audioSource.loop = true;
                //    audioSource.Play();
                //}
            }
            // ���ڸ��� �������� ��
            else
            {
                movement.MoveSpeed = 0;     // �̵� �ӵ��� 0����
                playerAnim.MoveSpeed = 0;   // playerAnim�� �ӵ��� 0���� => Idle �ִϸ��̼� ���

                // ������ �� ���尡 ������̸� ����
                //audioSource.Stop();
            }

            // �÷��̾��� ȸ���� * �̵� ����
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
                // �׿� ���� �ӵ� ����, �ִϸ��̼� ���, ���� ���
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                //audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // ����Ű �Է� ���δ� �� ������ Ȯ���ϱ� ������
                // ������� ���� �ٽ� ������� �ʵ��� isPlaying���� üũ�ؼ� ���
                //if (audioSource.isPlaying == false)
                //{
                //    audioSource.loop = true;
                //    audioSource.Play();
                //}
            }
            // ���ڸ��� �������� ��
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // ������ �� ���尡 ������̸� ����
                //audioSource.Stop();
            }

            movement.MoveTo(new Vector3(x2, 0, z2));
        }
    }

    // 1P�� 2P ����
    private void UpdateJump()
    {
        // Player 1
        if (player2P == false)
        {
            // ������ ������ ������ ��� ������ ����
            // ���� �����ʹ� �ٸ���
            if (Input.GetKey(keyCodeJump1P) && !isJump)
            {
                movement.Jump();                        // ����
                playerAnim.PlayTrigger("doJump");       // Ȱ��ȭ
                playerAnim.PlayBool("isJump", true);    // PlayerJumpAnimation
                //audioSource.clip = audioClipJump;       
                //audioSource.Play();
                isJump = true;                          // ������ �ϴ� ���̴� isJump�� true��
            }
            else
            {
                audioSource.Stop();
            }
        }

        // Player 2
        else if (player2P == true)
        {
            // �������� ������ ������ �� �����ϱ� ����
            if (Input.GetKeyDown(keyCodeJump2P) && !isJump)
            {
                movement.Jump();                        // ����
                playerAnim.PlayTrigger("doJump");       // Ȱ��ȭ
                playerAnim.PlayBool("isJump", true);    // PlayerJump Animation
                //audioSource.clip = audioClipJump;
                //audioSource.Play();
                isJump = true;                          // ������ �ϴ� ���̴� isJump�� true��
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    // CharacterController�� ��� ���� �� �浹�� �ޱ� ����
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Ư�� �±׸� ������ �ִ� ������Ʈ(��, �÷��̾�, �ٸ� ��)�� ���� �� ���� �ʱ�ȭ
        if (hit.gameObject.tag == "Ground" || hit.gameObject.tag == "Player" || hit.gameObject.tag == "Bridge" || hit.gameObject.tag == "Arrow" ||
            hit.gameObject.tag == "Lever" || hit.gameObject.tag == "Object" || hit.gameObject.tag == "Lever2" || hit.gameObject.tag == "Lever3" ||
            hit.gameObject.tag == "Tower" || hit.gameObject.tag == "DissappearGround")
        {
            // PlayerJump Animation�� ��Ȱ��ȭ ��Ű�鼭 PlayerLand Animtion�� Ȱ��ȭ�� ��
            // ExitNode�� ���� Movement Blend�� �ٽ� ����
            playerAnim.PlayBool("isJump", false);
            isJump = false;
        }
        // Player1�� Lever���
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever.LeverActivate();              // ���� �ִϸ��̼� ���
            hazardSpikeTrap.SpikeDisabled();    // HazardSpike �ִϸ��̼� ���
        }
        else if (hit.gameObject.tag == "Lever2" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever2.LeverActivate();                     // ���� �ִϸ��̼� ���
            transparency.gameObject.SetActive(true);    // ��Ȱ��ȭ ���� ���� Ȱ��ȭ
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever3.LeverActivate();
            tower.OpenDoor();
        }
        // Player2�� Lever���
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever.LeverActivate();          // ���� �ִϸ��̼� ���
            Destroy(spikyBallCollection);   // SpikyBall ����
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever3.LeverActivate(); // ���� �ִϸ��̼� ���
            tower.OpenDoor();       // TowerDoor �ִϸ��̼� ���
        }
    }

    // ������
    private void OnTriggerEnter(Collider other)
    {
        // ����
        if (other.tag == "Respawn")
        {
            movement.MoveSpeed = 0;     // �̵� �ӵ��� 0����
            playerAnim.MoveSpeed = 0;   // playerAnim�� �ӵ��� 0���� => Idle �ִϸ��̼� ���
            gameObject.transform.position = fallingTarget.transform.position;
        }
        else if (other.tag == "RespawnStage1")
        {
            // Player2�� �߰��� �޸���, ���� �Ұ� ������ �����Ƿ� ������ �ɶ� �ٽ� �� �� �ְ� ����
            if (player2P == true)
            {
                keyCodeRun2P = KeyCode.RightShift;
                keyCodeJump2P = KeyCode.Space;
            }
            movement.MoveSpeed = 0;     // �̵� �ӵ��� 0����
            playerAnim.MoveSpeed = 0;   // playerAnim�� �ӵ��� 0���� => Idle �ִϸ��̼� ���
            gameObject.transform.position = fallingStage1Target.transform.position;
        }
        else if (other.tag == "RespawnStage2")
        {
            movement.MoveSpeed = 0;     // �̵� �ӵ��� 0����
            playerAnim.MoveSpeed = 0;   // playerAnim�� �ӵ��� 0���� => Idle �ִϸ��̼� ���
            gameObject.transform.position = fallingStage2Target.transform.position;
        }
        // Player1�� ȭ��ǥ��� ���� �ʾ��� ��
        if (other.tag == "Respawn1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget.transform.position;
            }
        }
        else if (other.tag == "Respawn1_2")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;
            }
        }
        // Player2�� ȭ��ǥ��� ���� �ʾ��� ��
        if (other.tag == "Respawn2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget.transform.position;
            }
        }
        else if (other.tag == "Respawn2_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;
            }
        }
        // Player1�� ���� �Ѿ� �ڸ� ������ ��
        if (other.tag == "Respawn1_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage1Target.transform.position;
            }
        }
        else if (other.tag == "Respawn3_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage2Target.transform.position;
            }
        }
        // Player2�� ���� �Ѿ� �ڸ� ������ ��
        else if (other.tag == "Respawn2_1")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage1Target.transform.position;
            }
        }
        else if (other.tag == "Respawn3_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage2Target.transform.position;
            }
        }
        // JumpFalse��� �±׸� ���� �ݶ��̴��� ����� �� ������, �޸��⸦ ���ϰ� �ȴ�.
        if(other.tag == "JumpFalse" && player2P == true)
        {
            keyCodeRun2P = KeyCode.None;
            keyCodeJump2P = KeyCode.None;
        }
        else if(other.tag == "JumpTrue" && player2P == true)
        {
            keyCodeRun2P = KeyCode.RightShift;
            keyCodeJump2P = KeyCode.Space;
        }
        // Spike��� �±׸� ���� ������Ʈ�� �ε����� �� Stage1�� ���õ� Spike�̹Ƿ� fallingStage1Target���� ������
        if (other.tag == "Spike")
        {
            gameObject.transform.position = fallingStage1Target.transform.position;
        }
        // Saw��� �±׸� ���� ������Ʈ�� �ε����� �� Stage2�� ���õ� Saw�̹Ƿ� fallingStage2Target���� ������
        if (other.tag == "Saw")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;
        }
        // CannonBall�̶�� �±׸� ���� ������Ʈ�� �ε����� �� Stage2�� ���õ� CannonBall�̹Ƿ� fallingStage2Target���� ������
        if(other.tag == "CannonBall")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;
        }
    }


}
