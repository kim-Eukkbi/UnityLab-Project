using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : GenericSingleton<UIManager>
{
    public CanvasGroup gameoverPanel;

    [SerializeField] private Button fastStartButton;


    void Start()
    {
        fastStartButton.onClick.AddListener(() => SceneManager.LoadScene("Loading"));

        gameoverPanel.interactable = false;
        gameoverPanel.alpha = 0;
        gameoverPanel.blocksRaycasts = false;
    }

    public void OpenPanel(bool on)
    {
        gameoverPanel.alpha = on ? 1 : 0;
        gameoverPanel.interactable = on;
        gameoverPanel.blocksRaycasts = on;
    }
}
