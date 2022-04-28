using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBox : MonoBehaviour
{
    /// ����ǥ �ڽ��� ����� ��,                              -> �÷��̾ �±׿� �´� �ڽ��� ����� �� ȣ��
    /// �װ� ����ǥ �ڽ��� �ٲ��,                            -> ������Ʈ ����
    /// ������ ����,                                         -> ī��Ʈ�� �ڵ峻���� ���� ��Ű��
    /// ���� ������ �ϳ� ����,                                -> +1
    /// ���� ������ �ִ밹���� ���ٸ�,                        -> if(���� == �ִ밹��)
    /// �Ѿ �� �ְ�                                       -> ���Ƴ��� �ݶ��̴��� ����
    /// �� ���� �ʾҴٸ� ���Ѿ�� ���� �κ����� �ǵ��� ����   -> �ݶ��̴� �������� �±׸� ���� �� fallingStage2Target���� ����

    public float count;
    public GameObject blockCollider;
    public GameObject exclamationBox;
    public GameObject questionBox;
    private GameObject exclamation;
    private GameObject question;
    private GameObject changeBox;

    private void Start()
    {
        exclamation = GameObject.Find(exclamationBox.name);
        question = GameObject.Find(questionBox.name);
        changeBox = exclamation;
    }

    public void ChangeBoxExclamation()
    {
        exclamation = question;
    }

    public void ChangeBoxQuestion()
    {
        question = changeBox;
    }

}
