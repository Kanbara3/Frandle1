using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class Visitor : MonoBehaviour
{
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;
    private VisitorManager visitorManager;
    private FrandleLevelManager levelManager;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public int id;
    public int level;
    public int virtualLevel;
    public string visitorName;

    private Image visitorImage;

    private GameObject menu;
    private Button iconButton;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("Menu").transform.GetChild(id - 1).gameObject; //各キャラクターのメニューパネル取得
        iconButton = this.transform.GetChild(0).GetComponent<Button>(); //子オブジェクトのButtonを取得
        if(level > 0) iconButton.onClick.AddListener(ActivateMenu); //訪問者メニューを開く
        menu.GetComponent<VisitorMenu>().returnButton.onClick.AddListener(DeactivateMenu);  //訪問者メニューを閉じる
        menu.GetComponent<VisitorMenu>().levelUpButton.onClick.AddListener(LevelUp); // levelUpボタン
    }

    // Update is called once per frame
    void Update()
    {

    }

    //levelテキストの更新
    public void LevelTextUpdate()
    {
        levelText.text = "Lv." + level.ToString();
    }

    // menuPanelのアクティブ
    void ActivateMenu()
    {
        if(menu != null)
        {
            menu.SetActive(true);
        }
        menu.GetComponent<VisitorMenu>().LevelTextUpdate(level, virtualLevel);
    }
    // menuPanelの非アクティブ
    void DeactivateMenu()
    {
        if (menu != null)
        {
            menu.SetActive(false);
        }
    }

    // レベルアップボタン
    void LevelUp()
    {
        level++;
        virtualLevel++;
        LevelTextUpdate();
        menu.GetComponent<VisitorMenu>().LevelTextUpdate(level, virtualLevel);
        visitorManager.ApplyVisitorLevelBenefit(id, level);
        moneyManager.Pay(100);
        SetVisitorLevel();
    }

    public void GachaLevelUp()
    {
        level++;
        virtualLevel++;
        InitVisitor(id.ToString(), name);
        LevelTextUpdate();
    }

    // FrandleのLevelを上限に大きい方を選択し、Visitorのlevelに代入
    public void SetVisitorLevel()
    {
        if (levelManager.currentLevel > virtualLevel) return;
        int setLevel = Mathf.Min(virtualLevel, levelManager.currentLevel);
        level = setLevel;
        LevelTextUpdate();
        if(menu == null || menu.GetComponent<VisitorMenu>() == null) { return; }
        menu.GetComponent<VisitorMenu>().LevelTextUpdate(level, virtualLevel);
    }

    //Asset>Resources>CharacterImageフォルダから画像を読み込み
    public void InitVisitor(string imagePath, string visitorName)
    {
        visitorImage.sprite = Resources.Load<Sprite>("VisitorImage/" + (level == 0 ? "0" : imagePath));
        nameText.text = (level == 0 ? "?" : visitorName); //levelが0のときnameを"?"にする
    }

    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        visitorManager = GameObject.Find("VisitorManager").GetComponent<VisitorManager>();
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();

        visitorImage = this.transform.GetChild(0).GetComponent<Image>();
        nameText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        levelText = this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
