using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Lever lever;                     //
    public Lever lever2;                    // Lever 제어
    public Lever lever3;                    //
    public HazardSpikeTrap hazardSpikeTrap; // HazardSpikeTrap 제어
    public GameObject spikyBallCollection;  // SpikyBall 들을 가지고 있는 오브젝트
    public TransparencyGround transparency; // 안에 있는 Ground들을 관리
    public Tower tower;                     // Tower 제어
    public DisappearGround dissapearGround; // DissapearGround 제어
    public GameObject disappearObject;      // 생성된 DissapearGround
    public GameObject dissapearObject2;     // 생성된 DissapearGround
    public GameObject wrongObject;          // 오답 오브젝트
    public ChangeBox changeBox;             // ChangeBox 제어
    public GameObject bridgeObject;         // ChangeBox의 물음표 박스가 느낌표로 바뀌었을 때 생길 다리
    public Stair stair;                     // 계단
    public GameObject gameClearCheck;       // 게임을 클리어 했는지 체크하는 오브젝트
    public GameObject gameClearCheck2;      // 게임을 클리어 했는지 체크하는 오브젝트 2
    public Arrow arrow;                     // 성 바로 앞에 있는 화살표

    public GameClearUI clearUI;             // GameClearUI 제어
    public GameObject clearUIObject;        // 활성화 될 Player1의 GameObjectUI
    public GameObject clearUIObject2;       // 활성화 첫번째가 활성화 된 후 Player2의 ClearPanel을 활성화 시킬 GameObjectUI
    public Button clearButton;              // 활성화 될 ButtonObject
    public Button clearButton2;             // 활성화 될 ButtonObject

    public bool isJump;                     // 점프 상태 여부
    public bool player2P;                   // 1P와 2P를 구분
    public bool clearBool = false;          // 게임 클리어 시 움직일 bool값

    public GameObject fallingTarget;        // 낙사 했을 때 리스폰 될 TargetPos
    public GameObject bridgeTarget;         // 화살표대로 가지 않았을 때 리스폰될 TargetPos
    public GameObject bridgeTarget2;        // 화살표대로 가지 않았을 때 리스폰될 TargetPos
    public GameObject fallingStage1Target;  // Stage1에서 낙사를 하거나 Spike 태그를 가진 오브젝트에 닿았을 때 리스폰 될 TargetPos
    public GameObject fallingStage2Target;  // Stage2에서 낙사를 하거나 리스폰 될 TargetPos


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

    // 매 프레임마다 움직임과, 점프를 호출
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
                //audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // 방향키 입력 여부는 매 프레임 확인하기 때문에
                // 재생중일 때는 다시 재생하지 않도록 isPlaying으로 체크해서 재생
                //if (audioSource.isPlaying == false)
                //{
                //    audioSource.loop = true;
                //    audioSource.Play();
                //}
            }
            // 제자리에 멈춰있을 떄
            else
            {
                movement.MoveSpeed = 0;     // 이동 속도를 0으로
                playerAnim.MoveSpeed = 0;   // playerAnim의 속도를 0으로 => Idle 애니메이션 재생

                // 멈췄을 때 사운드가 재생중이면 정지
                //audioSource.Stop();
            }

            // 플레이어의 회전값 * 이동 방향
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
                //audioSource.clip = isRun == true ? audioClipRun : audioClipWalk;

                // 방향키 입력 여부는 매 프레임 확인하기 때문에
                // 재생중일 때는 다시 재생하지 않도록 isPlaying으로 체크해서 재생
                //if (audioSource.isPlaying == false)
                //{
                //    audioSource.loop = true;
                //    audioSource.Play();
                //}
            }
            // 제자리에 멈춰있을 떄
            else
            {
                movement.MoveSpeed = 0;
                playerAnim.MoveSpeed = 0;

                // 멈췄을 때 사운드가 재생중이면 정지
                //audioSource.Stop();
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
            //else
            //{
            //    audioSource.Stop();
            //}
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
        if (hit.gameObject.tag == "Ground" || hit.gameObject.tag == "Player" || hit.gameObject.tag == "Bridge" || hit.gameObject.tag == "Arrow" ||
            hit.gameObject.tag == "Lever" || hit.gameObject.tag == "Object" || hit.gameObject.tag == "Lever2" || hit.gameObject.tag == "Lever3" ||
            hit.gameObject.tag == "Tower" || hit.gameObject.tag == "DissappearGround" || hit.gameObject.tag == "Box")
        {
            // PlayerJump 애니메이션을 비활성화 시키면서 PlayerLand Animtion을 활성화한 후
            // ExitNode로 나가 Movement Blend로 다시 진입
            playerAnim.PlayBool("isJump", false);
            isJump = false;
        }
        // Spring이라는 태그에 닿았을 때
        //else if(hit.gameObject.tag == "Spring")
        //{
        //    // PlayerJump 애니메이션을 비활성화 시킴으로써 PlayerIdle 상태로 전환
        //    playerAnim.PlayBool("isJump", false);
        //    spring.PutSpring();                         // Spring 애니메이션 재생

        //}
        // DissappearGround라는 태그를 가진 땅에 닿았을 때
        if(hit.gameObject.tag == "DissappearGround")
        {
            // 일정 시간이 지난 후 그 땅을 삭제
            dissapearGround.DisappearGroundPlayer(disappearObject);
        }
        // Player1의 Lever사용
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever.LeverActivate();                      // 레버 애니메이션 재생
            GameObject.Find(hazardSpikeTrap.name).GetComponent<HazardSpikeTrap>().SpikeDisabled();
            //hazardSpikeTrap.SpikeDisabled();            // HazardSpike 애니메이션 재생
        }
        else if (hit.gameObject.tag == "Lever2" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever2.LeverActivate();                     // 레버 애니메이션 재생
            transparency.gameObject.SetActive(true);    // 비활성화 였던 지형 활성화
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.E) && player2P == false)
        {
            lever3.LeverActivate();                     // 레버 애니메이션 재생
            tower.OpenDoor();                           // TowerDoor 애니메이션 재생
            stair.gameObject.SetActive(true);
        }
        // Player2의 Lever사용
        if (hit.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever.LeverActivate();                      // 레버 애니메이션 재생
            Destroy(spikyBallCollection);               // SpikyBall 삭제
        }
        else if (hit.gameObject.tag == "Lever3" && Input.GetKeyDown(KeyCode.LeftBracket) && player2P == true)
        {
            lever3.LeverActivate();                     // 레버 애니메이션 재생
            tower.OpenDoor();                           // TowerDoor 애니메이션 재생
            arrow.gameObject.SetActive(true);           // ArrowObject 활성화
        }
        // ChangeBox 물음표 박스를 밟았을 때 느낌표로 바뀐다.
        if(hit.gameObject.tag == "Question")
        {
            changeBox.ChangeBoxQuestion();
            // bridgeObject 활성화
            bridgeObject.SetActive(true);
        }
        // Start이라는 태그를 가진 계단을 밟았을 때 점프가 안되게한다.
        if(hit.gameObject.tag == "Stair")
        {
            // PlayerJump 애니메이션을 비활성화시키면서 점프로 계단에 진입했을 시 점프가 끝난 후, 애니메이션을 비활성화 시켜야 걷는, 달리는 애니메이션이 재생된다.
            playerAnim.PlayBool("isJump", false);
            // 점프가 안되게 isJump를 true로 바꿈 (이러면 땅에 닿았을 때 다시 false로 바껴서 점프가 가능해진다.)
            isJump = true;
        }
        // Wrong이라는 태그를 가진 그라운드를 밟았을 때 사라지면서 떨어지게 한다
        if(hit.gameObject.tag == "Wrong")
        {
            Destroy(wrongObject);
        }
    }

    // 리스폰
    private void OnTriggerEnter(Collider other)
    {
        // 낙사
        if (other.tag == "Respawn")
        {
            gameObject.transform.position = fallingTarget.transform.position;           // 리스폰
        }
        else if (other.tag == "RespawnStage1")
        {
            // Player2는 중간에 달리기, 점프 불가 구간이 있으므로 리스폰 될때 다시 할 수 있게 설정
            if (player2P == true)
            {
                keyCodeRun2P = KeyCode.RightShift;
                keyCodeJump2P = KeyCode.Space;
            }
            gameObject.transform.position = fallingStage1Target.transform.position;     // 리스폰
        }
        else if (other.tag == "RespawnStage2")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // 리스폰
            dissapearGround.SpawnGround(disappearObject, dissapearObject2);
        }
        // Player1이 화살표대로 가지 않았을 때
        if (other.tag == "Respawn1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget.transform.position;        // 리스폰
            }
        }
        else if (other.tag == "Respawn1_2")
        {
            if (player2P == false)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;       // 리스폰
            }
        }
        // Player2가 화살표대로 가지 않았을 때
        if (other.tag == "Respawn2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget.transform.position;        // 리스폰
            }
        }
        else if (other.tag == "Respawn2_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = bridgeTarget2.transform.position;       // 리스폰
            }
        }
        // Player1이 벽을 넘어 뒤를 향했을 때
        if (other.tag == "Respawn1_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage1Target.transform.position; // 리스폰
            }
        }
        else if (other.tag == "Respawn3_1")
        {
            if (player2P == false)
            {
                gameObject.transform.position = fallingStage2Target.transform.position; // 리스폰
                dissapearGround.SpawnGround(disappearObject, dissapearObject2);
            }
        }
        // Player2가 벽을 넘어 뒤를 향했을 때
        else if (other.tag == "Respawn2_1")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage1Target.transform.position; // 리스폰
            }
        }
        else if (other.tag == "Respawn3_2")
        {
            if (player2P == true)
            {
                gameObject.transform.position = fallingStage2Target.transform.position; // 리스폰
                dissapearGround.SpawnGround(disappearObject, dissapearObject2);
            }
        }
        // JumpFalse라는 태그를 가진 콜라이더에 닿았을 때 점프와, 달리기를 못하게
        if (other.tag == "JumpFalse" && player2P == true)
        {
            keyCodeRun2P = KeyCode.None;
            keyCodeJump2P = KeyCode.None;
        }
        // JumpTrue라는 태그를 가진 콜라이더에 닿았을 때 점프와, 달리기를 다시 가능하게
        else if (other.tag == "JumpTrue" && player2P == true)
        {
            keyCodeRun2P = KeyCode.RightShift;
            keyCodeJump2P = KeyCode.Space;
        }
        // Spike라는 태그를 가진 오브젝트에 부딪혔을 때 Stage1에 관련된 Spike이므로 fallingStage1Target에서 리스폰
        if (other.tag == "Spike")
        {
            gameObject.transform.position = fallingStage1Target.transform.position;     // 리스폰
        }
        // Saw라는 태그를 가진 오브젝트에 부딪혔을 때 Stage2에 관련된 Saw이므로 fallingStage2Target에서 리스폰
        if (other.tag == "Saw")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // 리스폰
            dissapearGround.SpawnGround(disappearObject, dissapearObject2);
        }
        // CannonBall이라는 태그를 가진 오브젝트에 부딪혔을 때 Stage2에 관련된 CannonBall이므로 fallingStage2Target에서 리스폰
        if(other.tag == "CannonBall")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // 리스폰
        }
        // Block이라는 태그를 가진 콜라이더에 닿았을 때 Stage2에 관련되 BlockCollider이므로 fallingStage2Target에서 리스폰
        if(other.tag == "Block")
        {
            gameObject.transform.position = fallingStage2Target.transform.position;     // 리스폰
        }
        // TowerClear라는 태그를 가진 콜라이더에 닿았을 때 임시로 게임 클리어를 만들어놓았습니다
        if (other.tag == "TowerClear" && player2P == true)
        {
            // Player1의 UI창 띄우면서 임시 게임 클리어 창을 활성화
            clearUI.Clear(clearUIObject);
            // 게임을 클리어하는 콜라이더에 닿았을 때 그 콜라이더를 삭제 시킨다 -> 두번 닿지 않게 하기 위해
            DestroyCollider(gameClearCheck);
            // 두번째 성에 들어 갔을 때 첫번째 성 앞에 있는 화살표를 활성화 시킨다.
            arrow.gameObject.SetActive(true);
            // Bool값을 true로 바꾼다.
            clearBool = true;
        }
        // TowerClear라는 태그를 가진 콜라이더에 닿고, clearBool이 true일때
        if(other.tag == "TowerClear2" && clearBool == true && player2P == true)
        {
            // Player2의 클리어 창을 활성화
            clearUI.Clear(clearUIObject2);
            // 게임을 클리어 하는 콜라이더에 닿았을 때 그 콜라이더를 삭제 시킨다 -> 두번 닿지 않게 하기 위해
            DestroyCollider(gameClearCheck2);
            clearUI.ClearButton(clearButton);   // Player1의 클리어 버튼 활성화
            clearUI.ClearButton(clearButton2);  // Player2의 클리어 버튼 활성화
            // 게임 클리어 버튼을 눌러야 하니 마우스 커서를 활성화 시킨다.
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // 콜라이더를 삭제하는 메소드
    public void DestroyCollider(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
