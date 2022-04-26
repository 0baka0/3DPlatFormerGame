using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearGround : MonoBehaviour
{
    public GameObject dissapearGround1P;    // ����� ��
    public GameObject dissapearGround2P;    // ����� ��
                                            
    public Transform dissapearGroundParent; // �� ���� ������ ���� �θ� ������Ʈ

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        // ����
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    // ���� �� ����
    public void SpawnGround(GameObject dissapearGround1, GameObject dissapearGround2)
    {
        // ��� ���� �Ǹ� �ȵǴ� �̸� ������ ���� ������Ų �Ŀ� �ٽ� ����
        Destroy(GameObject.Find(dissapearGround1.name + "(Clone)"));
        Destroy(GameObject.Find(dissapearGround2.name + "(Clone)"));
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    public void FallingGroundPlayer()
    {
        GameObject.Find(dissapearGround1P.name + "(Clone)").transform.position = Vector3.down;
    }

    // ����
    public void DissapearGroundPlayer(GameObject dissapearGround)
    {
        // �ڷ�ƾ��ŭ �ð��� ��� �� ����
        StartCoroutine("DissapearGroundPlayerCoroutine", dissapearGround);
    }

    // ���� �ڷ�ƾ
    private IEnumerator DissapearGroundPlayerCoroutine(GameObject dissapearGround)
    {
        // .5�� ��� �� ����
        yield return new WaitForSeconds(.5f);
        Destroy(GameObject.Find(dissapearGround.name + "(Clone)"));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "RespawnStage2")
        {
            Destroy(GameObject.Find(dissapearGround1P.name + "(Clone)"));
            Destroy(GameObject.Find(dissapearGround2P.name + "(Clone)"));
        }
    }
}
