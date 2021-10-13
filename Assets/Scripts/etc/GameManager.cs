using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController pc;

    private void Awake()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if (instance != null)
        {
            Debug.Log("다수의 게임매니저가 실행중입니다");
        }
        instance = this;
    }

    public void GameOver()
    {
        if (UIManager.instance != null)
        {
            Time.timeScale = 0;
            UIManager.instance.OpenPanel(this.GetComponent<CanvasGroup>());
        }

        //더 할일이 있을 것 같으니 함수로 파둠
    }
}
