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

    // ����� �� 3�� �� �ϰ� �ϰ� �� �ؿ� �ݶ��̴��� ����� �� ����
    // �÷��̾ ���� ���� �� ��ġ ���� �°� �� ��ũ��Ʈ�� ������ �ִ� ������Ʈ�� �� ���� �� ����

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
