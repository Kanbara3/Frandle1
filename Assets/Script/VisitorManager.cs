using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

[System.Serializable]
public class VisitorJsonData
{
    public VisitorInfo[] visitorInfos;
}
[System.Serializable]
public class VisitorInfo
{
    public int id;
    public string name;
    public string enName;
    public string effect;
}

public class VisitorManager : MonoBehaviour
{
    public FoodManager foodManager;
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;
    public ToyManager toyManager;

    public GameObject visitorPrefab;
    public GameObject visitorContent;
    public List<GameObject> visitorList = new List<GameObject>();

    public GameObject menuPrefab;
    public GameObject menuContent;
    public Button visitorButton;

    public VisitorJsonData visitorJsonData;

    void Start()
    {
        readVisiterJson();
        foreach (var visitor in visitorJsonData.visitorInfos)
        {
            //VisitorPrefeab
            GameObject newVisitor = Instantiate(visitorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newVisitor.transform.SetParent(visitorContent.transform, false);
            newVisitor.GetComponent<Visitor>().InitVisitor(visitor.id.ToString(), visitor.name);
            newVisitor.GetComponent<Visitor>().id = visitor.id;
            newVisitor.GetComponent<Visitor>().name = visitor.name;
            visitorList.Add(newVisitor); //Prefab�I�u�W�F�N�g�̃��X�g�쐬
            //MenuPrefeab
            GameObject newMenu = Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newMenu.SetActive(false);
            newMenu.transform.SetParent (menuContent.transform, false);
            newMenu.GetComponent<VisitorMenu>().id = visitor.id;
            newMenu.GetComponent<VisitorMenu>().visitorName = visitor.name;
            newMenu.GetComponent<VisitorMenu>().name = "Menu" + visitor.enName;
            newMenu.GetComponent <VisitorMenu>().effectName = visitor.effect;
            
        }
        
        LoadVisitorLevel();
        GenerateImageIfLevelAboveOne();
        ApplyBenefit();
        UpdateLevelTextInVisitorPanel();
    }

    void Update()
    {
        SaveVisitorLevel();
        
    }

    //visitor��level�ɂ�鉶�b
    public void ApplyVisitorLevelBenefit(int id, int level)
    {
        // �얲�F�^�b�v�ŏ㏸���鐔�𑝂₷
        if (id == 1)
        {
            frandleManager.UpdateOneTapIncrease(level);
            //for ( int i=0; i<level; i++)
            //{
            //    frandleManager.UpdateOneTapIncrease(1);
            //}
        }
        // �������F���u���ɗ��܂�money�̏������
        if(id == 2)
        {
            moneyManager.MoneyIncreaseLimitBoost(level);
            //for ( int i=0; i<level; i++)
            //{
            //    moneyManager.MoneyIncreaseLimitBoost(1);
            //}
        }
        // ���[�~�A�F���͂���グ�����̍D���x�㏸(increaseXPRate)���u�[�X�g
        if (id == 3)
        {
            foodManager.IncreaseXPRateIncrease(level);
            //for ( int i=0; i<level; i++)
            //{
            //    foodManager.IncreaseXPRateIncrease(1);
            //}
        }
        // ��d���F�ݑ܊g��
        if (id == 4)
        {
            for ( int i=0; i<level; i++)
            {
                frandleManager.MaxSatietyIncrease(1);
            }
        }
        // �`���m�F��������̎��Ԃ�Z�k
        if (id == 5)
        {
            for ( int i=0; i<level; i++)
            {
                toyManager.TimeToPlayDecrease(1);
            }
        }
        // ����F�����x�̌������i
        if (id == 6)
        {
            for ( int i=0; i<level; i++)
            {
                frandleManager.BoostSatietyDecreaseRate(1);
            }
        }
        // �������F�①�Ɋg��
        if ( id == 7)
        {
            foodManager.ExpandFoodLimit(level);
            //for ( int i=0; i<level; i++)
            //{
            //    foodManager.ExpandFoodLimit(1);
            //}
        }
        // �p�`�����[�F�K�`���l����
        if (id == 8)
        {
            for ( int i=0; i<level; i++)
            {

            }
        }
        // ���F���͂�l����
        if (id == 9)
        {
            for ( int i=0; i<level; i++)
            {

            }
        }
        // ���~���A�F1�b�ő�����money�𑝉�
        if (id == 10)
        {
            for ( int i=0; i<level; i++)
            {
                moneyManager.MoneyIncreaseBoost(1);
            }
        }
    }

    // �N����ɑO��̉��b�܂�
    private void ApplyBenefit()
    {
        foreach (var visitors in visitorList)
        {
            var visitor = visitors.GetComponent<Visitor>();
            ApplyVisitorLevelBenefit(visitor.id, visitor.level);
        }
    }


    //Level�̃Z�[�u
    void SaveVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            //visitorLevel�̃Z�[�u
            int visitorLevel = visitorList[i].GetComponent<Visitor>().level;
            PlayerPrefs.SetInt("saveVisitorLevel_" + i, visitorLevel);
            //virtualLevel�̃Z�[�u
            int virtualLevel = visitorList[i].GetComponent<Visitor>().virtualLevel;
            PlayerPrefs.SetInt("saveVirtualLevel_" + i, virtualLevel);
        }
    }

    //Level�̃��[�h
    void LoadVisitorLevel()
    {
        for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
        {
            //visitorLevel�̃��[�h
            int visitorLevel = PlayerPrefs.GetInt("saveVisitorLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().level = visitorLevel;
            // virtualLevel�̃��[�h
            int virtualLevel = PlayerPrefs.GetInt("saveVirtualLevel_" + i, 0);
            visitorList[i].GetComponent<Visitor>().virtualLevel = virtualLevel;
            visitorList[i].GetComponent<Visitor>().LevelTextUpdate();
        }
    }

    // �K��҃p�l�����J��������level���X�V����
    void UpdateLevelTextInVisitorPanel()
    {
        visitorButton.onClick.AddListener(() =>
        {
            for (int i = 0; i < visitorJsonData.visitorInfos.Length; i++)
            {
                visitorList[i].GetComponent<Visitor>().SetVisitorLevel();
                Debug.Log("test");
            }
            
        });
    }

    //VisitorLevel������1�ȏ�̎��ɉ摜�𐶐�
    void GenerateImageIfLevelAboveOne()
    {
        Visitor[] visitors = visitorContent.GetComponentsInChildren<Visitor>();
        foreach (Visitor visitor in visitors)
        {
            if (visitor.level >= 1)
            {
                visitor.InitVisitor(visitor.id.ToString(), visitor.name);
            }
        }
    }

    // json�̓ǂݍ���
    void readVisiterJson()
    {
        string visiterJson = Resources.Load<TextAsset>("visitorJson").ToString();
        visitorJsonData = JsonUtility.FromJson<VisitorJsonData>(visiterJson);
    }

    
    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        toyManager = GameObject.Find("ToyManager").GetComponent<ToyManager>();
    }
}
