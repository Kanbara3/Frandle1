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
        menu = GameObject.Find("Menu").transform.GetChild(id - 1).gameObject; //�e�L�����N�^�[�̃��j���[�p�l���擾
        iconButton = this.transform.GetChild(0).GetComponent<Button>(); //�q�I�u�W�F�N�g��Button���擾
        if(level > 0) iconButton.onClick.AddListener(ActivateMenu); //�K��҃��j���[���J��
        menu.GetComponent<VisitorMenu>().returnButton.onClick.AddListener(DeactivateMenu);  //�K��҃��j���[�����
        menu.GetComponent<VisitorMenu>().levelUpButton.onClick.AddListener(LevelUp); // levelUp�{�^��
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

    // menuPanel�̃A�N�e�B�u
    void ActivateMenu()
    {
        if(menu != null)
        {
            menu.SetActive(true);
        }
        menu.GetComponent<VisitorMenu>().LevelTextUpdate(level, virtualLevel);
    }
    // menuPanel�̔�A�N�e�B�u
    void DeactivateMenu()
    {
        if (menu != null)
        {
            menu.SetActive(false);
        }
    }

    // ���x���A�b�v�{�^��
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

    // Frandle��Level������ɑ傫������I�����AVisitor��level�ɑ��
    public void SetVisitorLevel()
    {
        if (levelManager.currentLevel > virtualLevel) return;
        int setLevel = Mathf.Min(virtualLevel, levelManager.currentLevel);
        level = setLevel;
        LevelTextUpdate();
        if(menu == null || menu.GetComponent<VisitorMenu>() == null) { return; }
        menu.GetComponent<VisitorMenu>().LevelTextUpdate(level, virtualLevel);
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
        visitorManager = GameObject.Find("VisitorManager").GetComponent<VisitorManager>();
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();

        visitorImage = this.transform.GetChild(0).GetComponent<Image>();
        nameText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        levelText = this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
