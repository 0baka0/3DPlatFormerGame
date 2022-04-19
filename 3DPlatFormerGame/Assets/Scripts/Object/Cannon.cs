using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform cannonBallSpawnPoint;
    public Transform cannonBallParents;
    public GameObject cannonBallPrefab;
    public float cannonBallSpeed;

    private void Start()
    {
        StartCoroutine("CannonShootDelay", .7);
    }

    private IEnumerator CannonShootDelay(float delayTime)
    {
        var cannonBall = Instantiate(cannonBallPrefab, cannonBallSpawnPoint.transform.position, cannonBallPrefab.transform.rotation, cannonBallParents);
        cannonBall.GetComponent<Rigidbody>().velocity = cannonBallSpawnPoint.forward * cannonBallSpeed;

        yield return new WaitForSeconds(delayTime);

        StartCoroutine("CannonShootDelay", .7);
    }
}
