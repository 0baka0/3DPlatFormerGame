using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearUI : MonoBehaviour
{
    // 플레이어가 도착하면 움직임을 멈추고 다른 플레이어를 기다려야함
    // 도착했을 때 UI창을 띄움
    // bool 값 하나를 false일 때 true로 만듬
    // 다른 창이 또 활성화 됐을 때 이미 그 bool값이 true 일 때 GameClear버튼을 활성화 시킴

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
