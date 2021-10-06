using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup gameoverPanel;

    public static UIManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("다수의 게임매니저가 실행중입니다");
        }
        instance = this;
    }

    void Start()
    {
        gameoverPanel.interactable = false;
        gameoverPanel.alpha = 0;
        gameoverPanel.blocksRaycasts = false;
    }

    void Update()
    {
        
    }


    public void OpenPanel(bool on)
    {
        gameoverPanel.alpha = on ? 1 : 0;
        gameoverPanel.interactable = on;
        gameoverPanel.blocksRaycasts = on;
    }
}
