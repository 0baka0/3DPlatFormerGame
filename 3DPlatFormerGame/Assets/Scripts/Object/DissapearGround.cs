using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearGround : MonoBehaviour
{
    public GameObject dissapearGround1P;
    public GameObject dissapearGround2P;

    public Transform dissapearGroundParent;

    private void Start()
    {
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    public void DissapearGroundPlayer(GameObject dissapearGround)
    {
        StartCoroutine("DissapearGroundPlayerCoroutine", dissapearGround);
    }

    public void SpawnGround()
    {
        Instantiate(dissapearGround1P, dissapearGround1P.transform.position, dissapearGround1P.transform.rotation, dissapearGroundParent);
        Instantiate(dissapearGround2P, dissapearGround2P.transform.position, dissapearGround2P.transform.rotation, dissapearGroundParent);
    }

    private IEnumerator DissapearGroundPlayerCoroutine(GameObject dissapearGround)
    {
        yield return new WaitForSeconds(.5f);
        Destroy(GameObject.Find(dissapearGround.name + "(Clone)"));
    }
}
