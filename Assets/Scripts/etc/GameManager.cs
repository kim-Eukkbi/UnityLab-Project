using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController pc;

    public bool isDied = false;

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
        if (UIManager.instance != null && !isDied )
        {
            Time.timeScale = 0;
            UIManager.instance.OpenPanel(UIManager.instance.gameOverPanel);
            isDied = true;
        }

        //�� ������ ���� �� ������ �Լ��� �ĵ�
    }
}
