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
    public string visitorName;

    private Image visitorImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //level�e�L�X�g�̍X�V
    public void LevelTextUpdate()
    {
        levelText.text = "Lv." + level.ToString();
    }

    //Asset>Resources>CharacterImage�t�H���_����摜��ǂݍ���
    public void InitVisitor(string imagePath, string visitorName)
    {
        visitorImage.sprite = Resources.Load<Sprite>("VisitorImage/" + (level == 0 ? "0" : imagePath));
        nameText.text = (level == 0 ? "?" : visitorName); //level��0�̂Ƃ�name��"?"�ɂ���
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
