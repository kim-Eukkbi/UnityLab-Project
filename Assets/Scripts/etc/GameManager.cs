using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("�ټ��� ���ӸŴ����� �������Դϴ�");
        }
        instance = this;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        UIManager.instance.OpenPanel(true);
        //�� ������ ���� �� ������ �Լ��� �ĵ�
    }
}
