using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearGround : MonoBehaviour
{
    public GameObject disappearGround1P;    // ����� ��
    public GameObject disappearGround2P;    // ����� ��
                                            
    public Transform disappearGroundParent; // �� ���� ������ ���� �θ� ������Ʈ

    private void Start()
    {
        // ����
        Instantiate(disappearGround1P, disappearGround1P.transform.position, disappearGround1P.transform.rotation, disappearGroundParent);
        Instantiate(disappearGround2P, disappearGround2P.transform.position, disappearGround2P.transform.rotation, disappearGroundParent);
    }

    // ���� �� ����
    public void SpawnGround(GameObject disappearGround1, GameObject disappearGround2)
    {
        // ��� ���� �Ǹ� �ȵǴ� �̸� ������ ���� ������Ų �Ŀ� �ٽ� ����
        Destroy(GameObject.Find(disappearGround1.name + "(Clone)"));
        Destroy(GameObject.Find(disappearGround2.name + "(Clone)"));
        Instantiate(disappearGround1P, disappearGround1P.transform.position, disappearGround1P.transform.rotation, disappearGroundParent);
        Instantiate(disappearGround2P, disappearGround2P.transform.position, disappearGround2P.transform.rotation, disappearGroundParent);
    }

    // ����
    public void DisappearGroundPlayer(GameObject disappearGround)
    {
        // �ڷ�ƾ��ŭ �ð��� ��� �� ����
        StartCoroutine("DissapearGroundPlayerCoroutine", disappearGround);
    }

    // ���� �ڷ�ƾ
    private IEnumerator DissapearGroundPlayerCoroutine(GameObject disappearGround)
    {
        // .5�� ��� �� ����
        yield return new WaitForSeconds(.5f);
        Destroy(GameObject.Find(disappearGround.name + "(Clone)"));
    }
}
