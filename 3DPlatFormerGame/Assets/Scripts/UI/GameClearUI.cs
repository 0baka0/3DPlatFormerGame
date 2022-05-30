using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    public GameObject player1Clear;     // Player1�� Ŭ���� Panel
    public GameObject player2Clear;     // Player2�� Ŭ���� Panel
    public Button gameClear1PButton;    // Player1�� Ŭ���� ��ư
    public Button gameClear2PButton;    // Player2�� Ŭ���� ��ư

    private void Start()
    {
        player1Clear.SetActive(false);                  // ���� �� �� ��Ȱ��ȭ ��Ų��.
        player2Clear.SetActive(false);                  // ���� �� �� ��Ȱ��ȭ ��Ų��.
        gameClear1PButton.gameObject.SetActive(false);  // ���� �� �� ��Ȱ��ȭ ��Ų��.
        gameClear2PButton.gameObject.SetActive(false);  // ���� �� �� ��Ȱ��ȭ ��Ų��.
    }

    // Player1�� Player2�� �ٸ��� ������ ClearPanel�� ���� Ȱ��ȭ��Ų��.
    public void Clear(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    // Player1�� Player2�� �ٸ��� ������ ClearButton�� ���� Ȱ��ȭ��Ų��.
    public void ClearButton(Button button)
    {
        button.gameObject.SetActive(true);
    }

    // gameClearButton�� ������ �� ������ ���� ��Ų��.
    public void GameClear()
    {
        Application.Quit();
    }
}
