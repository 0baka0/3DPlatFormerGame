using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearGround : MonoBehaviour
{
    public GameObject dissapearGround1P;    // 사라질 땅
    public GameObject dissapearGround2P;    // 사라질 땅
                                            
    public Transform dissapearGroundParent; // 그 땅을 가지고 있을 부모 오브젝트

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponentInChildren<Rigidbody>();
    }

    private void Start()
    {
        // 생성
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    // 삭제 후 생성
    public void SpawnGround(GameObject dissapearGround1, GameObject dissapearGround2)
    {
        // 계속 생성 되면 안되니 미리 생성된 땅을 삭제시킨 후에 다시 생성
        Destroy(GameObject.Find(dissapearGround1.name + "(Clone)"));
        Destroy(GameObject.Find(dissapearGround2.name + "(Clone)"));
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    public void FallingGroundPlayer()
    {
        GameObject.Find(dissapearGround1P.name + "(Clone)").transform.position = Vector3.down;
    }

    // 삭제
    public void DissapearGroundPlayer(GameObject dissapearGround)
    {
        // 코루틴만큼 시간이 경과 후 삭제
        StartCoroutine("DissapearGroundPlayerCoroutine", dissapearGround);
    }

    // 삭제 코루틴
    private IEnumerator DissapearGroundPlayerCoroutine(GameObject dissapearGround)
    {
        // .5초 경과 후 삭제
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
