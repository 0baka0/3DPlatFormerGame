using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    // 낙사를 했을 때마다 리스폰 되게
    private void Start()
    {
        Instantiate(gameObject, transform.position, transform.rotation);
    }

    public void FallingGroundObject()
    {
        StartCoroutine("FallingGroundObejct");
    }

    public IEnumerator FallingGroundObejct()
    {
        gameObject.transform.position += Vector3.down * Time.deltaTime;
        yield return new WaitForSeconds(3f);
    }


}
