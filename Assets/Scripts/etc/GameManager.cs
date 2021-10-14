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
            Debug.Log("�ټ��� ���ӸŴ����� �������Դϴ�");
        }
        instance = this;
    }

    public void GameOver()
    {
        if (UIManager.instance != null)
        {
            Time.timeScale = 0;
            UIManager.instance.OpenPanel(UIManager.instance.gameOverPanel);
        }

        //�� ������ ���� �� ������ �Լ��� �ĵ�
    }
}
