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
    /// �� ���� �ʾҴٸ� ���Ѿ�� ���� �κ����� �ǵ��� ����   -> �ݶ��̴� �������� �±׸� ���� �� fallingStage2Target���� ���� V
    /// �Ѱ��� �ٲܼ� �ִµ� ���� ����̸� �ϳ��� �ٲ� �� ����
    /// �����ϰ� ���� �ϳ��� ������ �Ǵϱ� 

    public GameObject blockCollider;    // �� ���� ���� �ִ� ������Ʈ
    public GameObject exclamationBox;   // ����ǥ �ڽ� ������
    public GameObject questionBox;      // ����ǥ �ڽ� ������
    private GameObject exclamation;     // ����ǥ �ڽ��� ���� ������Ʈ
    private GameObject question;        // ����ǥ �ڽ��� ���� ������Ʈ

    public Transform changeBoxParent;   // ������ �ڽ��� ������ ���� �θ� ������Ʈ

    private void Start()
    {
        exclamation = GameObject.Find(exclamationBox.name); // �̸����� ��ġ�� ã�� ����
        question = GameObject.Find(questionBox.name);       // �̸����� ��ġ�� ã�� ����
    }

    // ����ǥ�ڽ� ������Ʈ�� ����ǥ�ڽ��� �ٲ۴�
    public void ChangeBoxQuestion()
    {
        // ����ǥ�ڽ� ������Ʈ�� ��ġ�� ����ǥ�ڽ� ����
        Instantiate(exclamationBox, question.transform.position, exclamationBox.transform.rotation, changeBoxParent);
        // ����ǥ �ڽ��� ����
        Destroy(question);
        // ���� ���� �ִ� ������Ʈ ����
        Destroy(blockCollider);
    }

}
