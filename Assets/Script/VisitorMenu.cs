using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VisitorMenu : MonoBehaviour
{
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI effectText;
    public int id;
    public string visitorName;
    private Image visitorImage;
    public Button returnButton;
    public Button levelUpButton;
    public string effectName;

    // Start is called before the first frame update
    void Start()
    {
        InitVisitor(id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Visitorからlevelを参照し表示
    public void LevelTextUpdate(int level, int virtualLevel)
    {
        levelText.text = "Lv." + level.ToString() + "(Lv." + virtualLevel + ")";
    }

    //Asset>Resources>CharacterImageフォルダから画像を読み込み
    public void InitVisitor(int id)
    {
        visitorImage.sprite = Resources.Load<Sprite>("VisitorImage/" + id);
        nameText.text = visitorName;
        effectText.text = effectName;
    }

    void Awake()
    {
        visitorImage = this.transform.GetChild(1).GetComponent<Image>();
        nameText = this.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        levelText = this.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        effectText = this.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        levelUpButton = this.transform.GetChild(6).GetComponent<Button>();
        returnButton = this.transform.GetChild(7).GetComponent<Button>();
    }
}
