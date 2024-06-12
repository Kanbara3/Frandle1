using UnityEngine;

public class BenefitManager : MonoBehaviour
{

    private FoodManager foodManager;
    private FrandleManager frandleManager;
    private MoneyManager moneyManager;
    public ToyManager toyManager;
    private GachaManager gachaManager;
    private FoodShop foodShop;

    public void ApplyVisitorLevelBenefit(int id, int level)
    {
        // �얲�F�^�b�v�ŏ㏸���鐔�𑝂₷
        if (id == 1)
        {
            frandleManager.UpdateOneTapIncrease(level, 1);
        }
        // �������F���u���ɗ��܂�money�̏������
        if (id == 2)
        {
            moneyManager.MoneyIncreaseLimitBoost(level, 100);
        }
        // ���[�~�A�F���͂���グ�����̍D���x�㏸(increaseXPRate)���u�[�X�g
        if (id == 3)
        {
            float foodXpUpRate = 1 + (float)level / 100;
            foodManager.foodXpIncreaseUpdate(foodXpUpRate, level);
        }
        // ��d���F�ݑ܊g��
        if (id == 4)
        {
            frandleManager.MaxSatietyIncrease(level, 50);
        }

        // �`���m�F��������̌��ʂ��A�b�v
        if (id == 5)
        {
            toyManager.RaiseTheBenefitOfToy(level, 1);
        }

        // ����F�����x�̌������i
        if (id == 6)
        {
            frandleManager.BoostSatietyDecreaseRate(level, 1);

        }
        // �������F�①�Ɋg��
        if (id == 7)
        {
            foodManager.ExpandFoodLimit(level, 20);
        }
        // �p�`�����[�F�K�`���l����
        if (id == 8)
        {
            gachaManager.DiscountTicketPrice(level);

        }
        // ���F���͂�l����
        if (id == 9)
        {
            foodShop.DiscountFoodPrice(level);
        }
        // ���~���A�F1�b�ő�����money�𑝉�
        if (id == 10)
        {
            moneyManager.MoneyIncreaseBoost(level, 1);
        }
    }

    void Awake()
    {
        frandleManager = GameObject.Find("Frandle").GetComponent<FrandleManager>();
        foodManager = GameObject.Find("FoodManager").GetComponent <FoodManager>();
        moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        toyManager = GameObject.Find("ToyManager").GetComponent<ToyManager>();
        gachaManager = GameObject.Find("GachaManager").GetComponent<GachaManager>();
        foodShop = GameObject.Find("FoodShop").GetComponent<FoodShop>();
    }
}
