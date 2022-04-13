 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSaw : MonoBehaviour
{
    Vector3 pos; // 현재위치
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    public float speed; // 이동속도

    void Start()
    {
        // pos에 현재 위치를 넣음
        pos = transform.position;
    }

    void Update()
    {
        // 현재 위치를 저장
        Vector3 v = pos;

        // 이해중
        v.x += delta * Mathf.Sin(Time.time * speed);

        // 현재위치에 계산된 v값을 저장
        transform.position = v;
    }
}
