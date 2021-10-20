using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController pc;

    public bool isDied = false;
    public int bestScore;
    public int score = 0;

    private float time;

    private void Awake()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        if (instance != null)
        {
            Debug.Log("�ټ��� ���ӸŴ����� �������Դϴ�");
        }
        instance = this;
        UIManager.instance.OpenPanel_NoneStack(UIManager.instance.inGamePenel);
        bestScore = PlayerPrefs.GetInt("BestScore");
    }
	private void Update()
    {
        time += Time.deltaTime;

    }
    public int SetScore()
    {
        return score = Mathf.FloorToInt(time);
    }
    public void SaveBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }
	public void GameOver()
    {
        if (UIManager.instance != null && !isDied )
        {
            Time.timeScale = 0;
            SaveBestScore();
            UIManager.instance.OpenPanel(UIManager.instance.gameOverPanel);
            isDied = true;
        }

        //�� ������ ���� �� ������ �Լ��� �ĵ�
    }
}
