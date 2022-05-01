using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBox : MonoBehaviour
{
    /// 물음표 박스를 밟았을 때,                              -> 플레이어가 태그에 맞는 박스를 밟았을 때 호출
    /// 그게 느낌표 박스로 바뀌고,                            -> 오브젝트 변경
    /// 갯수를 지정,                                         -> 카운트를 코드내에서 지정 시키고
    /// 밟을 때마다 하나 증가,                                -> +1
    /// 지정 갯수가 최대갯수와 같다면,                        -> if(변수 == 최대갯수)
    /// 넘어갈 수 있게                                       -> 막아놓은 콜라이더를 삭제
    /// 다 밟지 않았다면 못넘어가고 시작 부분으로 되돌아 오게   -> 콜라이더 생성으로 태그를 지정 후 fallingStage2Target으로 가게 V
    /// 한개는 바꿀수 있는데 저런 방식이면 하나만 바꿀 수 있음
    /// 생성하고 삭제 하나만 저렇게 되니까 

    public float count;
    public GameObject blockCollider;
    public GameObject exclamationBox;
    public GameObject questionBox;
    private GameObject exclamation;
    private GameObject question;
    private GameObject changeBox;

    public Transform changeBoxParent;

    private void Start()
    {
        exclamation = GameObject.Find(exclamationBox.name);
        question = GameObject.Find(questionBox.name);
        changeBox = exclamation;
    }

    public void ChangeBoxExclamation()
    {
        Instantiate(question, exclamation.transform.position, question.transform.rotation, changeBoxParent);
        Destroy(exclamation);
    }

    public void ChangeBoxQuestion()
    {
        Instantiate(exclamationBox, question.transform.position, exclamationBox.transform.rotation, changeBoxParent);
        Destroy(question);
    }

}
