using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;

public class FrandleManager : MonoBehaviour
{
    private FrandleLevelManager levelManager;
    private TapParticle tapParticle;

    public long XP=0; // ����
    public GameObject harttext;
    public GameObject kankeitext;
    public long oneTapIncrease = 1;
    public int satiety; //�����x
    public int satietyiMmutable; //�ς��Ȃ������x
    public int MAX_SATIETY = 100;
    private int satietyDecreaseRate = 0;�@//���b�Ŏg�p �����x�������[�g�ύX
    public long sliderXP;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();
        sliderXP = XP;
        //GiveButton();
        InvokeRepeating("DecreaseSatietyOverTime", 0f, 1f);

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(XP);
        //levelManager.FrandleLevelUp(tap);
        //KankeiDirector();
        DecreaseSatietyOverTime();
    }

    //�����x��MAX_SATIETY�𒴂��Ȃ��悤�ɑ��₷
    public void SatietyIncreaseRate(int upSatiety)
    {
        if(satiety+upSatiety < MAX_SATIETY)
        {
            if (satiety == 0)
            {
                satietyiMmutable = 0;
            }
            satiety += upSatiety;
            satietyiMmutable += upSatiety;
        }
    }

    private DateTime lastMeal; //�H��̎���
    private TimeSpan elapsedTime; //�o�ߎ���
    // �����x��1�b���ƂɌ��炷
    public void DecreaseSatietyOverTime()
    {
        if (satiety > 0)
        {

            elapsedTime = DateTime.UtcNow - lastMeal;

        }
        satiety = Mathf.Max(0, satietyiMmutable - (int)elapsedTime.TotalSeconds * satietyDecreaseRate);
        if (satiety == 0)
        {
            lastMeal = DateTime.UtcNow;
        }
        
    }

    // �K��҂̉��b��MAX_SATIETY�𑝉�
    public void MaxSatietyIncrease(int upRate, int initialValue)
    {
        MAX_SATIETY = upRate + initialValue;
    }

    // �K��҂̉��b��satietyDecreaseRate�𑝉�
    public void BoostSatietyDecreaseRate(int upRate, int initialValue)
    {
        satietyDecreaseRate = upRate + initialValue;
    }

    // XP�𑝂₷
    public void GainXP(long gainXP)
    {
        XP += gainXP;
        UpdateHeartUI();
    }

    //�����x�̃Z�[�u
    public void SaveSatiety()
    {
        DateTime lastMeal2 = DateTime.UtcNow; //�H��̎��Ԃ��L�^
        PlayerPrefs.SetInt("saveSatiety", satiety);
        PlayerPrefs.SetString("saveLastMeal", lastMeal2.ToString());
    }

    //�����x�̃��[�h
    public void LoadSatiety()
    {
        satiety = PlayerPrefs.GetInt("saveSatiety", 0);
        satietyiMmutable = satiety;
        string lastMealString = PlayerPrefs.GetString("saveLastMeal", "");
        lastMeal  = DateTime.Parse(lastMealString);
    }

    // �D���x(�o���l)�̍X�V �V�V�X�e��
    public void upFavourableImpression(long upRate)
    { 
        XP += upRate;
        sliderXP += upRate;
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();
    }

    // ���͂�������čD���x�Ɩ����x���㏸
    public void EatFood(long upXP, int upSatiety)
    {
        upFavourableImpression(upXP);
        SatietyIncreaseRate(upSatiety);
    }

    public bool CapableToEat(int satietyIncreaseRate)
    {
        if (satiety + satietyIncreaseRate >= MAX_SATIETY) return false;
        return true;
    }

    // oneTapIncrease�̍X�V
    public void UpdateOneTapIncrease(long upRate, long initialValue)
    {
        oneTapIncrease = upRate + initialValue;
    }

    // Background��Frandle��EventTrigger�ɃA�^�b�`
    public void HartDirector()
    {
        XP += oneTapIncrease;
        sliderXP += oneTapIncrease;
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();
        tapParticle.TapHartParticle();
    }

    // �D���x�Z�[�u
    public void SaveTap()
    {
        PlayerPrefs.SetString("saveTap", XP.ToString());
    }
    //�D���x���[�h
    public void LoadTap()
    {
        XP = long.Parse(PlayerPrefs.GetString("saveTap", (000).ToString()));
        levelManager.FrandleLevelUp(XP);
        UpdateHeartUI();

    }

    public int GetFrandleLevel()
    {
        return levelManager.currentLevel;
    }

    //harttext�̍X�V
    public void UpdateHeartUI()
    {
        this.harttext.GetComponent<TextMeshProUGUI>().text = XP.ToString("F0");
        levelManager.FrandleLevelUp(XP);
    }

    private void Awake()
    {
        levelManager = GameObject.Find("FrandleLevelManager").GetComponent<FrandleLevelManager>();
        tapParticle = GameObject.Find("TapParticleManager").GetComponent<TapParticle>();
    }

    // �D���x�ɂ���Ċ֌W�e�L�X�g���ω�����
    //public void KankeiDirector()
    //{
    //    if (tap < 1000) // 1
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�E�E�E�H";
    //    }
    //    else if (tap < 2000) // 2
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��邪�u���Ă���������";
    //    }
    //    else if (tap < 5000) // 3
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��������H";
    //    }
    //    else if (tap < 10000) // 4
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "��������Ȃ̂ɂ悭����������";
    //    }
    //    else if (tap < 20000) // 5
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���킢���Ȃ���������󂵂��Ⴄ����";
    //    }
    //    else if (tap < 50000) // 6
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁH";
    //    }
    //    else if (tap < 100000) // 7
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���������Ƃ���l��";
    //    }
    //    else if (tap < 300000) // 8
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�H�ׂĂ����l�Ԃ���";
    //    }
    //    else if (tap < 600000) // 9
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���ɐH�ׂ���_���Ȃ������Ⴞ���Č���ꂽ";
    //    }
    //    else if (tap < 1000000) // 10
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�����ɓ������l�Ԃ͂��Ȃ��ŎO�l�ڂ�";
    //    }
    //    else if (tap < 3000000) // 11 
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�V��ł�Ԃɉ�ꂿ����Ă��d���Ȃ����";
    //    }
    //    else if (tap < 6000000) // 12
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�Ȃɂ��ėV�ԁH";
    //    }
    //    else if (tap < 10000000) // 13
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁI����ǂ�ŁI";
    //    }
    //    else if (tap < 30000000) // 14
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁ`���l�`��ꂿ�����";
    //    }
    //    else if (tap < 60000000) // 15
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�������[����";
    //    }
    //    else if (tap < 100000000) // 16
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���g�̐l�Ԃ���z�������Ăǂ�Ȗ�����";
    //    }
    //    else if (tap < 300000000) // 17
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�l�ԁc����Ȃ��Ă��Ɂ[����H";
    //    }
    //    else if (tap < 500000000) // 18
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���Ɂ[����V��ŁI";
    //    }
    //    else if (tap < 800000000) // 19
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���o�l���Ă��Ɂ[����ƈ���Ă��������";
    //    }
    //    else if (tap < 1000000000) // 20
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "�˂��A�z���Ă����ł���H";
    //    }
    //    else if (tap < 2000000000) // 20
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���[���Ǝ��̂�������ˁI";
    //    }
    //    else // 21
    //    {
    //        this.kankeitext.GetComponent<TextMeshProUGUI>().text = "���[���Ǝ��̂�������ˁI";
    //    }
    //}
}