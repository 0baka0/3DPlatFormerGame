using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    public GameObject player1Clear;     // Player1의 클리어 Panel
    public GameObject player2Clear;     // Player2의 클리어 Panel
    public Button gameClear1PButton;    // Player1의 클리어 버튼
    public Button gameClear2PButton;    // Player2의 클리어 버튼

    private void Start()
    {
        player1Clear.SetActive(false);                  // 시작 할 때 비활성화 시킨다.
        player2Clear.SetActive(false);                  // 시작 할 때 비활성화 시킨다.
        gameClear1PButton.gameObject.SetActive(false);  // 시작 할 때 비활성화 시킨다.
        gameClear2PButton.gameObject.SetActive(false);  // 시작 할 때 비활성화 시킨다.
    }

    // Player1과 Player2가 다르기 때문에 ClearPanel을 따로 활성화시킨다.
    public void Clear(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    // Player1과 Player2가 다르기 때문에 ClearButton을 따로 활성화시킨다.
    public void ClearButton(Button button)
    {
        button.gameObject.SetActive(true);
    }

    // gameClearButton을 눌렀을 때 게임을 종료 시킨다.
    public void GameClear()
    {
        Application.Quit();
    }
}
