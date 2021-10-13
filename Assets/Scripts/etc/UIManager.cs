using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : GenericSingleton<UIManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    bool toggle = false;
    float originX = 0;
    string selectedScene = "";
    public GameObject player;


    [Header("게임시작화면")]
    [SerializeField] private CanvasGroup startPanel;
    [SerializeField] private RectTransform selectPanel;
    [SerializeField] private Button fastStartButton;
    [SerializeField] private Button selectPanelOnBtn;
    [SerializeField] private Text startText;
    [SerializeField] private Button easy;
    [SerializeField] private Button normal;
    [SerializeField] private Button hard;
    [SerializeField] private Button hell;

    [Space(15)]
    [Header("게임오버")]
    [SerializeField] private CanvasGroup gameOverPanel;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button selectDifficultyBtn;
    [SerializeField] private Button backMenuBtn;

    [Space(15)]
    [Header("난이도 선택")]
    [SerializeField] private CanvasGroup selectDifficultyPanel;
    [SerializeField] private Button selectEasy;
    [SerializeField] private Button selectNormal;
    [SerializeField] private Button selectHard;
    [SerializeField] private Button selectHell;

    void Start()
    {
        fastStartButton.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        selectPanelOnBtn.onClick.AddListener(() =>
        {
            float destX = 0;
            if (!toggle)
            {
                originX = selectPanel.localPosition.x;
                destX = originX - 400f;
            }
            else
            {
                destX = originX;
            }
            toggle = !toggle;

            Sequence seq = DOTween.Sequence();
            seq.Append(selectPanel.DOLocalMoveX(destX, 0.5f));

        });
        retryBtn.onClick.AddListener(() => LoadingManager.LoadScene(LoadingManager.GetSceneName()));
        selectDifficultyBtn.onClick.AddListener(() => OpenPanel(selectDifficultyPanel));
        backMenuBtn.onClick.AddListener(()=> LoadingManager.LoadScene("UICreate"));
        easy.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        normal.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        hard.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        hell.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));

        selectEasy.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        selectNormal.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        selectHard.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        selectHell.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        TextBlink(startText, 0.3f, 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel(gameOverPanel);
            player.GetComponent<PlayerInput>().speed = gameOverPanel.interactable ? 0 : 10;
        }

    }

    public void ConfirmOn()
    {
        LoadingManager.LoadScene(selectedScene);
    }

    public void OpenPanel(CanvasGroup canvas) // 알파, 상호작용, 터치 
    {
        bool on = !canvas.interactable;
        canvas.alpha = on ? 1 : 0;
        canvas.interactable = on;
        canvas.blocksRaycasts = on;
    }

    private void TextBlink(Text text, float endAlpha, float duration) //깜빡이는 텍스트
    {
        DOTween.Sequence().Append(text.DOFade(endAlpha, duration)).SetLoops(-1, LoopType.Yoyo);
    }
}
