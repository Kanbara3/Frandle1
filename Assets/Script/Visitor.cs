using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Visitor : MonoBehaviour
{
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public int id;
    public int level;

    private Image visitorImage;

    // Start is called before the first frame update
    void Start()
    {

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

    //Asset>Resources>CharacterImageフォルダから画像を読み込み
    public void InitVisitor(string imagePath, string visitorName)
    {
        visitorImage.sprite = Resources.Load<Sprite>("VisitorImage/" + imagePath);
        nameText.text = visitorName;

    }

    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();

        visitorImage = this.transform.GetChild(0).GetComponent<Image>();
        nameText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        levelText = this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
