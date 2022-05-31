using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Lever lever;                     //
    public Lever lever2;                    // Lever ����
    public Lever lever3;                    //
    public HazardSpikeTrap hazardSpikeTrap; // HazardSpikeTrap ����
    public GameObject spikyBallCollection;  // SpikyBall ���� ������ �ִ� ������Ʈ
    public TransparencyGround transparency; // �ȿ� �ִ� Ground���� ����
    public Tower tower;                     // Tower ����
    public DisappearGround dissapearGround; // DissapearGround ����
    public GameObject disappearObject;      // ������ DissapearGround
    public GameObject dissapearObject2;     // ������ DissapearGround
    public GameObject wrongObject;          // ���� ������Ʈ
    public ChangeBox changeBox;             // ChangeBox ����
    public GameObject bridgeObject;         // ChangeBox�� ����ǥ �ڽ��� ����ǥ�� �ٲ���� �� ���� �ٸ�
    public Stair stair;                     // ���
    public GameObject gameClearCheck;       // ������ Ŭ���� �ߴ��� üũ�ϴ� ������Ʈ
    public GameObject gameClearCheck2;      // ������ Ŭ���� �ߴ��� üũ�ϴ� ������Ʈ 2
    public Arrow arrow;                     // �� �ٷ� �տ� �ִ� ȭ��ǥ

    public GameClearUI clearUI;             // GameClearUI ����
    public GameObject clearUIObject;        // Ȱ��ȭ �� Player1�� GameObjectUI
    public GameObject clearUIObject2;       // Ȱ��ȭ ù��°�� Ȱ��ȭ �� �� Player2�� ClearPanel�� Ȱ��ȭ ��ų GameObjectUI
    public Button clearButton;              // Ȱ��ȭ �� ButtonObject
    public Button clearButton2;             // Ȱ��ȭ �� ButtonObject

    public bool isJump;                     // ���� ���� ����
    public bool player2P;                   // 1P�� 2P�� ����
    public bool clearBool = false;          // ���� Ŭ���� �� ������ bool��

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

    // �� �����Ӹ��� �����Ӱ�, ������ ȣ��
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
            //else
            //{
            //    audioSource.Stop();
            //}
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
            hit.gameObject.tag == "Tower" || hit.gameObject.tag == "DissappearGround" || hit.gameObject.tag == "Box")
        {
            // PlayerJump �ִϸ��̼��� ��Ȱ��ȭ ��Ű�鼭 PlayerLand Animtion�� Ȱ��ȭ�� ��
            // ExitNode�� ���� Movement Blend�� �ٽ� ����
            playerAnim.PlayBool("isJump", false);
            isJump = false;
        }
        // Spring�̶�� �±׿� ����� ��
        //else if(hit.gameObject.tag == "Spring")
        //{
        //    // PlayerJump �ִϸ��̼��� ��Ȱ��ȭ ��Ŵ���ν� PlayerIdle ���·� ��ȯ
        //    playerAnim.PlayBool("isJump", false);
        //    spring.PutSpring();                         // Spring �ִϸ��̼� ���

        //}
        // DissappearGround��� �±׸� ���� ���� ����� ��
        if(hit.gameObject.tag == "DissappearGround")
        {
            // ���� �ð��� ���� �� �� ���� ����
            dissapearGround.DisappearGroundPlayer(disappearObject);
        }
        // Player1�� Lever���
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever.LeverActivate();                      // ���� �ִϸ��̼� ���
            GameObject.Find(hazardSpikeTrap.name).GetComponent<HazardSpikeTrap>().SpikeDisabled();
            //hazardSpikeTrap.SpikeDisabled();            // HazardSpike �ִϸ��̼� ���
        }
        else if (hit.gameObject.tag == "Lever2" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever2.LeverActivate();                     // ���� �ִϸ��̼� ���
            transparency.gameObject.SetActive(true);    // ��Ȱ��ȭ ���� ���� Ȱ��ȭ
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever3.LeverActivate();                     // ���� �ִϸ��̼� ���
            tower.OpenDoor();                           // TowerDoor �ִϸ��̼� ���
            stair.gameObject.SetActive(true);
        }
        // Player2�� Lever���
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever.LeverActivate();                      // ���� �ִϸ��̼� ���
            Destroy(spikyBallCollection);               // SpikyBall ����
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever3.LeverActivate();                     // ���� �ִϸ��̼� ���
            tower.OpenDoor();                           // TowerDoor �ִϸ��̼� ���
        }
        // ChangeBox ����ǥ �ڽ��� ����� �� ����ǥ�� �ٲ��.
        if(hit.gameObject.tag == "Question")
        {
            changeBox.ChangeBoxQuestion();
            // bridgeObject Ȱ��ȭ
            bridgeObject.SetActive(true);
        }
        // Start�̶�� �±׸� ���� ����� ����� �� ������ �ȵǰ��Ѵ�.
        if(hit.gameObject.tag == "Stair")
        {
            // PlayerJump �ִϸ��̼��� ��Ȱ��ȭ��Ű�鼭 ������ ��ܿ� �������� �� ������ ���� ��, �ִϸ��̼��� ��Ȱ��ȭ ���Ѿ� �ȴ�, �޸��� �ִϸ��̼��� ����ȴ�.
            playerAnim.PlayBool("isJump", false);
            // ������ �ȵǰ� isJump�� true�� �ٲ� (�̷��� ���� ����� �� �ٽ� false�� �ٲ��� ������ ����������.)
            isJump = true;
        }
        // Wrong�̶�� �±׸� ���� �׶��带 ����� �� ������鼭 �������� �Ѵ�
        if(hit.gameObject.tag == "Wrong")
        {
            Destroy(wrongObject);
        }
    }

    // ������
    private void OnTriggerEnter(Collider other)
    {
        // ����
        if (other.tag == "Respawn")
        {
            gameObject.transform.position = fallingTarget.transform.position;           // ������
        }
        else if (other.tag == "RespawnStage1")
        {
            // Player2�� �߰��� �޸���, ���� �Ұ� ������ �����Ƿ� ������ �ɶ� �ٽ� �� �� �ְ� ����
            if (player2P == true)
            {
                keyCodeRun2P = KeyCode.RightShift;
                keyCodeJump2P = KeyCode.Space;
            }
            gameObject.transform.position = fallingStage1Target.transform.position;     // ������
        }
        else if (other.tag == "RespawnStage2")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // ������
            dissapearGround.SpawnGround(disappearObject, dissapearObject2);
        }
        // Player1�� ȭ��ǥ��� ���� �ʾ��� ��
        if (other.tag == "Respawn1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget.transform.position;        // ������
            }
        }
        else if (other.tag == "Respawn1_2")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;       // ������
            }
        }
        // Player2�� ȭ��ǥ��� ���� �ʾ��� ��
        if (other.tag == "Respawn2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget.transform.position;        // ������
            }
        }
        else if (other.tag == "Respawn2_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;       // ������
            }
        }
        // Player1�� ���� �Ѿ� �ڸ� ������ ��
        if (other.tag == "Respawn1_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage1Target.transform.position; // ������
            }
        }
        else if (other.tag == "Respawn3_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage2Target.transform.position; // ������
                dissapearGround.SpawnGround(disappearObject, dissapearObject2);
            }
        }
        // Player2�� ���� �Ѿ� �ڸ� ������ ��
        else if (other.tag == "Respawn2_1")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage1Target.transform.position; // ������
            }
        }
        else if (other.tag == "Respawn3_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage2Target.transform.position; // ������
                dissapearGround.SpawnGround(disappearObject, dissapearObject2);
            }
        }
        // JumpFalse��� �±׸� ���� �ݶ��̴��� ����� �� ������, �޸��⸦ ���ϰ�
        if (other.tag == "JumpFalse" && player2P == true)
        {
            keyCodeRun2P = KeyCode.None;
            keyCodeJump2P = KeyCode.None;
        }
        // JumpTrue��� �±׸� ���� �ݶ��̴��� ����� �� ������, �޸��⸦ �ٽ� �����ϰ�
        else if (other.tag == "JumpTrue" && player2P == true)
        {
            keyCodeRun2P = KeyCode.RightShift;
            keyCodeJump2P = KeyCode.Space;
        }
        // Spike��� �±׸� ���� ������Ʈ�� �ε����� �� Stage1�� ���õ� Spike�̹Ƿ� fallingStage1Target���� ������
        if (other.tag == "Spike")
        {
            gameObject.transform.position = fallingStage1Target.transform.position;     // ������
        }
        // Saw��� �±׸� ���� ������Ʈ�� �ε����� �� Stage2�� ���õ� Saw�̹Ƿ� fallingStage2Target���� ������
        if (other.tag == "Saw")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // ������
            dissapearGround.SpawnGround(disappearObject, dissapearObject2);
        }
        // CannonBall�̶�� �±׸� ���� ������Ʈ�� �ε����� �� Stage2�� ���õ� CannonBall�̹Ƿ� fallingStage2Target���� ������
        if(other.tag == "CannonBall")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // ������
        }
        // Block�̶�� �±׸� ���� �ݶ��̴��� ����� �� Stage2�� ���õ� BlockCollider�̹Ƿ� fallingStage2Target���� ������
        if(other.tag == "Block")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // ������
        }
        // TowerClear��� �±׸� ���� �ݶ��̴��� ����� �� �ӽ÷� ���� Ŭ��� �������ҽ��ϴ�
        if (other.tag == "TowerClear" && player2P == true)
        {
            // Player1�� UIâ ���鼭 �ӽ� ���� Ŭ���� â�� Ȱ��ȭ
            clearUI.Clear(clearUIObject);
            // ������ Ŭ�����ϴ� �ݶ��̴��� ����� �� �� �ݶ��̴��� ���� ��Ų�� -> �ι� ���� �ʰ� �ϱ� ����
            DestroyCollider(gameClearCheck);
            // �ι�° ���� ��� ���� �� ù��° �� �տ� �ִ� ȭ��ǥ�� Ȱ��ȭ ��Ų��.
            arrow.gameObject.SetActive(true);
            // Bool���� true�� �ٲ۴�.
            clearBool = true;
        }
        // TowerClear��� �±׸� ���� �ݶ��̴��� ���, clearBool�� true�϶�
        if(other.tag == "TowerClear2" && clearBool == true && player2P == true)
        {
            // Player2�� Ŭ���� â�� Ȱ��ȭ
            clearUI.Clear(clearUIObject2);
            // ������ Ŭ���� �ϴ� �ݶ��̴��� ����� �� �� �ݶ��̴��� ���� ��Ų�� -> �ι� ���� �ʰ� �ϱ� ����
            DestroyCollider(gameClearCheck2);
            clearUI.ClearButton(clearButton);   // Player1�� Ŭ���� ��ư Ȱ��ȭ
            clearUI.ClearButton(clearButton2);  // Player2�� Ŭ���� ��ư Ȱ��ȭ
            // ���� Ŭ���� ��ư�� ������ �ϴ� ���콺 Ŀ���� Ȱ��ȭ ��Ų��.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // �ݶ��̴��� �����ϴ� �޼ҵ�
    public void DestroyCollider(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
