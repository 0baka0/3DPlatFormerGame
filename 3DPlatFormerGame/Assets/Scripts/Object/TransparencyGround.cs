using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyGround : MonoBehaviour
{
    // 처음엔 비활성화 되있는 상태로 시작한다
    // 그 후 레버를 당겼을 때 활성화 된다.
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
