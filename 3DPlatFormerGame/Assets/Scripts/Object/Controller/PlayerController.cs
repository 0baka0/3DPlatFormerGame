using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input KeyCodes")]
    public KeyCode keyCodeRun = KeyCode.LeftShift;  // 달리기 키

    [Header("Audio Clips Afterwards Change")]
    public AudioClip audioClipWalk;                 // 걷기 사운드
    public AudioClip audioClipRun;                  // 달리기 사운드

    private PlayerMovement movement;                // 키보드 입력으로 플레이어 이동, 점프
    private Status status;                          // 이동속도 등의 플레이어 정보
    private PlayerAnim playerAnim;                  // 애니메이션 재생 제어
    private AudioSource audioSource;                // 사운드 재생 제어

    public bool player2P;

    private void Awake()
    {
        // 마우스 커서를 보이지 않게 설정하고, 현재 위치에 고정시킨다
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
    }

    // 이동
    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");       // a키 = 왼쪽 이동, d키 오른쪽 이동
        float z = Input.GetAxisRaw("Vertical");         // w키 = 위쪽 이동, s키 아래쪽 이동

        float x2 = Input.GetAxisRaw("Horizontal2P");    // l키 = 왼쪽 이동, '키 오른쪽 이동
        float z2 = Input.GetAxisRaw("Vertical2P");      // p키 = 위쪽 이동, ;키 아래쪽 이동

        // 이동중일 때 (걷기 or 뛰기)
        if (x != 0 || z != 0)
        {
            // 달리는 중인지 확인하는 bool 값
            bool isRun = false;

            // keyCodeRun(LeftShift)를 눌렀을 때 달린다
            isRun = Input.GetKey(keyCodeRun);

            // 쉬프트가 눌렸다면 달리기, 그게 아니라면 걷기
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
}
