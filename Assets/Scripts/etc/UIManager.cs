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
    public GameObject player = null;

    public Stack<CanvasGroup> panelStack = new Stack<CanvasGroup>();


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
    public CanvasGroup gameOverPanel;
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
        //fastStartButton.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        selectPanelOnBtn.onClick.AddListener(() =>
        {
            ButtonsPanelControll();
            
        });
        retryBtn.onClick.AddListener(() =>
        {
            PanelAllOff();
            Time.timeScale = 1;
            LoadingManager.LoadScene(LoadingManager.GetSceneName());
            GameManager.instance.isDied = false;
        });

        selectDifficultyBtn.onClick.AddListener(() =>
        {
            OffPanel(gameOverPanel);
            panelStack.Pop();
            OpenPanel(selectDifficultyPanel);
        });

        backMenuBtn.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            OpenPanel(startPanel);
            OpenPanel(selectPanel.gameObject.GetComponent<CanvasGroup>());
            OffPanel(gameOverPanel);
            LoadingManager.LoadScene("Start");

            Sequence seq = DOTween.Sequence();
            seq.Append(selectPanel.DOLocalMoveX(originX, 0f));
            toggle = !toggle;

            selectPanelOnBtn.gameObject.transform.Rotate(0, 0, gameObject.transform.rotation.z + 180);
            GameManager.instance.isDied = false;

        });

        easy.onClick.AddListener(() =>
        {
            PanelAllOff();
            Time.timeScale = 1;
            LoadingManager.LoadScene("InGame");

        });
        //normal.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        //hard.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        //hell.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));

        selectEasy.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            PanelAllOff();
            for(int i = 0; i < panelStack.Count; i++)
            {
                panelStack.Pop();
            }
            LoadingManager.LoadScene("InGame");
            GameManager.instance.isDied = false;
        });
        //selectNormal.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        //selectHard.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        //selectHell.onClick.AddListener(() => LoadingManager.LoadScene("UICreate"));
        TextBlink(startText, 0.3f, 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance != null && !GameManager.instance.isDied)
        {
            if (panelStack.Count <= 0  )
            {
                Time.timeScale = 0;
                OpenPanel(gameOverPanel);
            }
            else if (panelStack.Count == 1)
            {
                OffPanel(panelStack);
                Time.timeScale = 1;
            }
            else if(panelStack.Count > 1)
            {
                OffPanel(panelStack);
            }
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
        if (on) panelStack.Push(canvas);
    }

    private void TextBlink(Text text, float endAlpha, float duration) //깜빡이는 텍스트
    {
        DOTween.Sequence().Append(text.DOFade(endAlpha, duration)).SetLoops(-1, LoopType.Yoyo);
    }

    void PanelAllOff()
    {
        OffPanel(startPanel);
        OffPanel(gameOverPanel);
        OffPanel(selectDifficultyPanel);
        OffPanel(selectPanel.gameObject.GetComponent<CanvasGroup>());
    }

    void OffPanel(CanvasGroup canvas)
    {
        
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    void OffPanel(Stack<CanvasGroup> c)
    {
        OffPanel(c.Peek());
        c.Pop();
    }

    bool canDrag = true; // 버튼 패널 갖다 쓸 수 있는가

    void ButtonsPanelControll()
    {
        if (canDrag)
        {
            canDrag = false;
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
            seq.Append(selectPanel.DOLocalMoveX(destX, 0.5f)).OnComplete(() => canDrag = true); 
            selectPanelOnBtn.gameObject.transform.Rotate(0, 0, gameObject.transform.rotation.z + 180);
        }
    }
}
