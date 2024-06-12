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
        // 霊夢：タップで上昇する数を増やす
        if (id == 1)
        {
            frandleManager.UpdateOneTapIncrease(level, 1);
        }
        // 魔理沙：放置中に溜まるmoneyの上限増加
        if (id == 2)
        {
            moneyManager.MoneyIncreaseLimitBoost(level, 100);
        }
        // ルーミア：ごはんを上げた時の好感度上昇(increaseXPRate)をブースト
        if (id == 3)
        {
            float foodXpUpRate = 1 + (float)level / 100;
            foodManager.foodXpIncreaseUpdate(foodXpUpRate, level);
        }
        // 大妖精：胃袋拡張
        if (id == 4)
        {
            frandleManager.MaxSatietyIncrease(level, 50);
        }

        // チルノ：おもちゃの効果をアップ
        if (id == 5)
        {
            toyManager.RaiseTheBenefitOfToy(level, 1);
        }

        // 美鈴：満腹度の減少促進
        if (id == 6)
        {
            frandleManager.BoostSatietyDecreaseRate(level, 1);

        }
        // 小悪魔：冷蔵庫拡張
        if (id == 7)
        {
            foodManager.ExpandFoodLimit(level, 20);
        }
        // パチュリー：ガチャ値下げ
        if (id == 8)
        {
            gachaManager.DiscountTicketPrice(level);

        }
        // 咲夜：ごはん値下げ
        if (id == 9)
        {
            foodShop.DiscountFoodPrice(level);
        }
        // レミリア：1秒で増えるmoneyを増加
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
