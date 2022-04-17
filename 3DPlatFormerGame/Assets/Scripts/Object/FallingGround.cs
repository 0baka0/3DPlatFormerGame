using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    public GameObject spawnGrassSinglePrefab;
    public GameObject spawnGrassSinglePrefab2P;
    public Transform spawnGrassSingleParent;
    Vector3 spawnGrassSingleVector = new Vector3(13, 1, 175);
    Vector3 spawnGrassSingleVector2P = new Vector3(-13, 1, 175);

    // 밟았을 때 3초 후 하강 하강 후 밑에 콜라이더에 닿았을 때 삭제
    // 플레이어가 낙사 했을 때 위치 값에 맞게 이 스크립트를 가지고 있는 오브젝트는 다 삭제 후 생성

    private void Start()
    {
        SpawnGround();
    }

    public void SpawnGround()
    {
        Instantiate(spawnGrassSinglePrefab, spawnGrassSingleVector, spawnGrassSinglePrefab.transform.rotation, spawnGrassSingleParent);
        Instantiate(spawnGrassSinglePrefab2P, spawnGrassSingleVector2P, spawnGrassSinglePrefab2P.transform.rotation, spawnGrassSingleParent);
    }

    public void FallingGroundObject()
    {
        StartCoroutine("FallingGroundObejctCoroutine");
    }

    private IEnumerator FallingGroundObejctCoroutine()
    {
        //spawnGrassSinglePrefab.transform.position += Vector3.down * Time.deltaTime;
        //spawnGrassSinglePrefab2P.transform.position += Vector3.down * Time.deltaTime;

        Destroy(spawnGrassSinglePrefab);
        Destroy(spawnGrassSinglePrefab2P);

        yield return new WaitForSeconds(3f);
    }


}
