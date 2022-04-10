using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSaw : MonoBehaviour
{
    public float rightMax;  // 좌로 이동가능한 (x)최댓값
    public float leftMax;   // 우로 이동가능한 (x)최댓값
    public float y;         // Y값
    public float z;         // Z값
    float currentPosition;  // 현재 위치(x) 저장
    public float direction; // 이동속도 + 방향

    private void Start()
    {
        currentPosition = transform.position.x;
    }

    private void Update()
    {
        currentPosition += direction * Time.deltaTime;
        // 현재 위치가 우로 이동 가능한 (x)최댓값보다 크거나 같다면
        // direction에 -1을 곱해 반전을 해주고 현재 위치를 우로 이동가능한 (x)최댓박으로 설정
        if(currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
        }
        // 현재 위치가 좌로 이동가능한 (x)최댓값보다 크거나 같다면
        // direction에 -1을 곱해 반전을 해주고 현재 위치를 좌로 이동가능한 (x)최대값으로 설정
        else if(currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
        }
        // HazardSaw의 위치를 계산된 현재 위치로 처리
        transform.position = new Vector3(currentPosition, y, z);
    }
}
