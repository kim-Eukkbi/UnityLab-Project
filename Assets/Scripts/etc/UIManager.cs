using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : GenericSingleton<UIManager>
{
    

    [Header("게임시작화면")]
    [SerializeField] private CanvasGroup startPanel;
    [SerializeField] private CanvasGroup difficultySelectPanel;
    [SerializeField] private RectTransform selectPanel;
    [SerializeField] private Button fastStartButton;
    [SerializeField] private Button selectPanelOnBtn;
    [SerializeField] private Text startText;
    bool toggle = false;
    float originX = 0;

    [Space(15)]
    [Header("게임시작화면")]
    [SerializeField] private CanvasGroup gameoverPanel;
    [SerializeField] private CanvasGroup gameOverPanel;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button selectDifficultyBtn;
    [SerializeField] private Button backMenuBtn;

    [Space(15)]
    [Header("게임시작화면")]
    [SerializeField] private CanvasGroup selectDifficultyPanel;
    [SerializeField] private Button selectEasy  ;
    [SerializeField] private Button selectNormal;
    [SerializeField] private Button selectHard  ;
    [SerializeField] private Button selectHell  ;

    void Start()
    {
        fastStartButton.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));

        selectPanelOnBtn.onClick.AddListener(() =>
        {
            float destX = 0;
            if (!toggle)
            {
                originX = selectPanel.localPosition.x;
                destX = -0.2f;
            }
            else
            {
                destX = originX;
            }
            toggle = !toggle;

            Sequence seq = DOTween.Sequence();
            seq.Append(selectPanel.DOLocalMoveX(destX, 1f));

        });

        retryBtn.onClick.AddListener(() => LoadingManager.LoadScene(LoadingManager.GetSceneName()));
        selectDifficultyBtn.onClick.AddListener(() => selectDifficultyPanel.enabled = true);

        selectEasy.onClick.AddListener(() => LoadingManager.LoadScene(""));
        selectNormal.onClick.AddListener(() => LoadingManager.LoadScene(""));
        selectHard.onClick.AddListener(() => LoadingManager.LoadScene(""));
        selectHell.onClick.AddListener(() => LoadingManager.LoadScene(""));
        TextBlink(startText,0.3f,2f);

    }

    public void OpenPanel(bool on) // 알파, 상호작용, 터치 
    {
        gameoverPanel.alpha = on ? 1 : 0;
        gameoverPanel.interactable = on;
        gameoverPanel.blocksRaycasts = on;
    }

    private void TextBlink(Text text,float endAlpha,float duration) //깜빡이는 텍스트
    {
        DOTween.Sequence().Append(text.DOFade(endAlpha, duration)).SetLoops(-1, LoopType.Yoyo);
    }

    
}
