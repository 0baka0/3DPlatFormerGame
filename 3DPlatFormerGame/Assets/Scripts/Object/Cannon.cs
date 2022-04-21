using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonBallSpawnPoint;  // cannonBall의 스폰 위치
    public Transform cannonBallParents;     // cannonBall을 가지고 있을 부모 오브젝트
    public GameObject cannonBallPrefab;     // cannonBall 프리팹
    public float cannonBallSpeed;           // cannonBall의 속도

    private void Start()
    {
        // delayTime에 따라 쏜다.
        StartCoroutine("CannonShootDelay", .3);
    }

    // cannonBallPrefab을 Cannon에서 쏘게 만드는 코루틴
    private IEnumerator CannonShootDelay(float delayTime)
    {
        // cannonBall에 cannonBallPrefab를 생성, 스폰 위치는 cannonBallSpawnPoint, 회전값은 cannonBallPrefab의 회전값, 자식오브젝트로 할 부모오브젝트의 Transform 을 저장
        var cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.transform.position, cannonBallPrefab.transform.rotation, cannonBallParents);
        // cannoBallSpawnPoint의 앞으로 cannonBallSpeed만큼 이동
        cannonBall.GetComponent<Rigidbody>().velocity = cannonBallSpawnPoint.forward * cannonBallSpeed;

        // delayTime
        yield return new WaitForSeconds(delayTime);

        // 이렇게 적는 걸로 무한 반복이 이루어진다
        StartCoroutine("CannonShootDelay", .3);
    }
}
