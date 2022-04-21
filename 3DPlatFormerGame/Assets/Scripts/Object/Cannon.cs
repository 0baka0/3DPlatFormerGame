using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonBallSpawnPoint;  // cannonBall�� ���� ��ġ
    public Transform cannonBallParents;     // cannonBall�� ������ ���� �θ� ������Ʈ
    public GameObject cannonBallPrefab;     // cannonBall ������
    public float cannonBallSpeed;           // cannonBall�� �ӵ�

    private void Start()
    {
        // delayTime�� ���� ���.
        StartCoroutine("CannonShootDelay", .3);
    }

    // cannonBallPrefab�� Cannon���� ��� ����� �ڷ�ƾ
    private IEnumerator CannonShootDelay(float delayTime)
    {
        // cannonBall�� cannonBallPrefab�� ����, ���� ��ġ�� cannonBallSpawnPoint, ȸ������ cannonBallPrefab�� ȸ����, �ڽĿ�����Ʈ�� �� �θ������Ʈ�� Transform �� ����
        var cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.transform.position, cannonBallPrefab.transform.rotation, cannonBallParents);
        // cannoBallSpawnPoint�� ������ cannonBallSpeed��ŭ �̵�
        cannonBall.GetComponent<Rigidbody>().velocity = cannonBallSpawnPoint.forward * cannonBallSpeed;

        // delayTime
        yield return new WaitForSeconds(delayTime);

        // �̷��� ���� �ɷ� ���� �ݺ��� �̷������
        StartCoroutine("CannonShootDelay", .3);
    }
}
