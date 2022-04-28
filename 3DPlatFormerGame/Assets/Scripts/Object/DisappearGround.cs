using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearGround : MonoBehaviour
{
    public GameObject disappearGround1P;    // 사라질 땅
    public GameObject disappearGround2P;    // 사라질 땅
                                            
    public Transform disappearGroundParent; // 그 땅을 가지고 있을 부모 오브젝트

    private void Start()
    {
        // 생성
        Instantiate(disappearGround1P, disappearGround1P.transform.position, disappearGround1P.transform.rotation, disappearGroundParent);
        Instantiate(disappearGround2P, disappearGround2P.transform.position, disappearGround2P.transform.rotation, disappearGroundParent);
    }

    // 삭제 후 생성
    public void SpawnGround(GameObject disappearGround1, GameObject disappearGround2)
    {
        // 계속 생성 되면 안되니 미리 생성된 땅을 삭제시킨 후에 다시 생성
        Destroy(GameObject.Find(disappearGround1.name + "(Clone)"));
        Destroy(GameObject.Find(disappearGround2.name + "(Clone)"));
        Instantiate(disappearGround1P, disappearGround1P.transform.position, disappearGround1P.transform.rotation, disappearGroundParent);
        Instantiate(disappearGround2P, disappearGround2P.transform.position, disappearGround2P.transform.rotation, disappearGroundParent);
    }

    // 삭제
    public void DisappearGroundPlayer(GameObject disappearGround)
    {
        // 코루틴만큼 시간이 경과 후 삭제
        StartCoroutine("DissapearGroundPlayerCoroutine", disappearGround);
    }

    // 삭제 코루틴
    private IEnumerator DissapearGroundPlayerCoroutine(GameObject disappearGround)
    {
        // .5초 경과 후 삭제
        yield return new WaitForSeconds(.5f);
        Destroy(GameObject.Find(disappearGround.name + "(Clone)"));
    }
}
