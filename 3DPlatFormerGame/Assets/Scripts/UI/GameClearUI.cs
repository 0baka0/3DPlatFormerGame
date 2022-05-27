using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    // �÷��̾ �����ϸ� �������� ���߰� �ٸ� �÷��̾ ��ٷ�����
    // �������� �� UIâ�� ���
    // bool �� �ϳ��� false�� �� true�� ����
    // �ٸ� â�� �� Ȱ��ȭ ���� �� �̹� �� bool���� true �� �� GameClear��ư�� Ȱ��ȭ ��Ŵ

    public GameObject player1Clear;
    public GameObject player2Clear;
    public Button gameClear1PButton;
    public Button gameClear2PButton;

    private void Start()
    {
        player1Clear.SetActive(false);
        player2Clear.SetActive(false);
        gameClear1PButton.gameObject.SetActive(false);
        gameClear2PButton.gameObject.SetActive(false);
    }

    public void Clear(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void ClearButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}
