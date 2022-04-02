using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    private KeyCode keyCodeRun1P = KeyCode.LeftShift;   // 달리기 키 1P
    private KeyCode keyCodeRun2P = KeyCode.RightShift;  // 달리기 키 2P
    private KeyCode keyCodeJump1P = KeyCode.V;          // 점프 키 1P
    private KeyCode keyCodeJump2P = KeyCode.Space;      // 점프 키 2P

    [Header("Audio Clips Afterwards Change")]
    public AudioClip audioClipWalk;         // 걷기 사운드
    public AudioClip audioClipRun;          // 달리기 사운드
    public AudioClip audioClipJump;         // 점프 사운드

    private PlayerMovement movement;        // 키보드 입력으로 플레이어 이동, 점프
    private Status status;                  // 이동속도 등의 플레이어 정보
    private PlayerAnim playerAnim;          // 애니메이션 재생 제어
    private AudioSource audioSource;        // 사운드 재생 제어
    public Lever lever;                     // Lever 제어
    public HazardSpikeTrap[] hazardSpikeTrap; // HazardSpikeTrap 제어

    public bool isJump;                     // 점프 상태 여부
    public bool player2P;                   // 1P와 2P를 구분

    public GameObject fallingTarget;        // 낙사 했을 때 리스폰 될 TargetPos
    public GameObject bridgeTarget;         // 화살표대로 가지 않았을 때 리스폰될 TargetPos
    public GameObject fallingStage1Target;  // Stage1에서 낙사 했을 때 리스폰 될 TargetPos

    private void Awake()
    {
        // 마우스 커서를 보이지 않게 설정하고, 현재 위치에 고정시킨다
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        movement = GetComponent<PlayerMovement>();
        status = GetComponent<Status>();
        playerAnim = GetComponent<PlayerAnim>();
        audioSource = GetComponent<AudioSource>();
        //hazardSpikeTrap = GetComponent<HazardSpikeTrap>();
    }

    private void Update()
    {
        UpdateMove();
        UpdateJump();
    }

    // 1P와 2P 이동
    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");       // a키 = 왼쪽 이동, d키 오른쪽 이동
        float z = Input.GetAxisRaw("Vertical");         // w키 = 위쪽 이동, s키 아래쪽 이동

        float x2 = Input.GetAxisRaw("Horizontal2P");    // l키 = 왼쪽 이동, '키 오른쪽 이동
        float z2 = Input.GetAxisRaw("Vertical2P");      // p키 = 위쪽 이동, ;키 아래쪽 이동

        // Player 1
        if (player2P == false)
        {
            // 이동중일 때 (걷기 or 뛰기)
            if (x != 0 || z != 0)
            {
                // 달리는 중인지 확인하는 bool 값
                bool isRun = false;

                // keyCodeRun(LeftShift)를 눌렀을 때 달린다
                isRun = Input.GetKey(keyCodeRun1P);

                // 왼쪽 쉬프트가 눌렸다면 달리기, 그게 아니라면 걷기
                // 그에 따른 속도 증가, 애니메이션 재생, 사운드 재생
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // 방향키 입력 여부는 매 프레임 확인하기 때문에
                // 재생중일 때는 다시 재생하지 않도록 isPlaying으로 체크해서 재생
                if (audioSource.isPlaying == false)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            // 제자리에 멈춰있을 떄
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // 멈췄을 때 사운드가 재생중이면 정지
                audioSource.Stop();
            }

            movement.MoveTo(new Vector3(x, 0, z));
        }

        // Player 2
        else if (player2P == true)
        {
            // 이동중일 때 (걷기 or 뛰기)
            if (x2 != 0 || z2 != 0)
            {
                // 달리는 중인지 확인하는 bool 값
                bool isRun = false;

                // keyCodeRun2P(RightShift)를 눌렀을 때 달린다
                isRun = Input.GetKey(keyCodeRun2P);

                // 오른쪽 쉬프트가 눌렸다면 달리기, 그게 아니라면 걷기
                // 그에 따른 속도 증가, 애니메이션 재생, 사운드 재생
                movement.MoveSpeed = isRun == true ? status.runSpeed : status.walkSpeed;
                playerAnim.MoveSpeed = isRun == true ? 1 : .5f;
                audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // 방향키 입력 여부는 매 프레임 확인하기 때문에
                // 재생중일 때는 다시 재생하지 않도록 isPlaying으로 체크해서 재생
                if (audioSource.isPlaying == false)
                {
                    audioSource.loop = true;
                    audioSource.Play();
                }
            }
            // 제자리에 멈춰있을 떄
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // 멈췄을 때 사운드가 재생중이면 정지
                audioSource.Stop();
            }

            movement.MoveTo(new Vector3(x2, 0, z2));
        }
    }

    // 1P와 2P 점프
    private void UpdateJump()
    {
        // Player 1
        if (player2P == false)
        {
            // 점프를 누르고 있으면 계속 점프가 가능
            // 무한 점프와는 다르다
            if (Input.GetKey(keyCodeJump1P) && !isJump)
            {
                movement.Jump();                        // 점프
                playerAnim.PlayTrigger("doJump");       // 활성화
                playerAnim.PlayBool("isJump", true);    // PlayerJumpAnimation
                //audioSource.clip = audioClipJump;       
                //audioSource.Play();
                isJump = true;                          // 점프를 하는 중이니 isJump를 true로
            }
            else
            {
                audioSource.Stop();
            }
        }

        // Player 2
        else if (player2P == true)
        {
            // 연속으로 점프가 눌리는 걸 방지하기 위함
            if (Input.GetKeyDown(keyCodeJump2P) && !isJump)
            {
                movement.Jump();                        // 점프
                playerAnim.PlayTrigger("doJump");       // 활성화
                playerAnim.PlayBool("isJump", true);    // PlayerJump Animation
                //audioSource.clip = audioClipJump;
                //audioSource.Play();
                isJump = true;                          // 점프를 하는 중이니 isJump를 true로
            }
            else
            {
                audioSource.Stop();
            }
        }
    }

    // CharacterController를 사용 했을 때 충돌을 받기 위함
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // 특정 태그를 가지고 있는 오브젝트(땅, 플레이어, 다리 등)를 밟을 때 점프 초기화
        if (hit.gameObject.tag == "Ground" || hit.gameObject.tag == "Player" || hit.gameObject.tag == "Bridge" || hit.gameObject.tag == "Arrow" || hit.gameObject.tag == "Lever" || hit.gameObject.tag == "Object")
        {
            // PlayerJump Animation을 비활성화 시키면서 PlayerLand Animtion을 활성화한 후
            // ExitNode로 나가 Movement Blend로 다시 진입
            playerAnim.PlayBool("isJump", false);
            isJump = false;
        }
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever.LeverActivate();
            for (var i = 0; i < hazardSpikeTrap.Length; i++)
            {
                hazardSpikeTrap[i].SpikeDisabled();

            }
        }
    }

    // 리스폰
    private void OnTriggerEnter(Collider other)
    {
        // 낙사
        if (other.tag == "Respawn")
        {
            gameObject.transform.position = fallingTarget.transform.position;
        }
        else if (other.tag == "RespawnStage1")
        {
            gameObject.transform.position = fallingStage1Target.transform.position;
        }
        // Player2가 화살표대로 가지 않았을 때
        else if (other.tag == "Respawn1")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget.transform.position;
            }
        }
        // Player1이 화살표대로 가지 않았을 때
        else if (other.tag == "Respawn2")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget.transform.position;
            }
        }
        else if (other.tag == "Spike")
        {
            gameObject.transform.position = fallingStage1Target.transform.position;
        }
    }


}
