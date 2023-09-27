using PSW.Core.Stat;
using UnityEngine;

public class MysteryResult : MonoBehaviour
{
    #region EVENT01 - Starting
    public void Event01_ObtainCash()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.cashGain);

        if (Player.Instance == null) return;

        Player.Instance.ObtainCash(250);
    }

    public void Event01_ObtainRelic()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        // ÀÏ¹Ý µî±Þ À¯¹° 1°³ È¹µæ
        var relic = GameManager.Instance.GetRandomRelic(1);
        Player.Instance.AddItemRelic(relic);
    }

    public void Event01_ObtainPotions()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        // ÀÏ¹Ý µî±Þ Æ÷¼Ç 2°³ È¹µæ
        for (int i = 0; i < 2; i++)
        {
            var potion = GameManager.Instance.GetRandomPotion(1);
            Player.Instance.AddItemPotion(potion);
        }
    }

    public void Event01_ReceiveGold()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.cashGain);

        if (Player.Instance == null) return;

        Player.Instance.ObtainCash(100);
    }
    #endregion

    #region EVENT02 - Trap
    public void Event02_Left()
    {
        MysterySystem.Instance.EventEnd("After_1");
    }

    public void Event02_Right()
    {
        MysterySystem.Instance.EventEnd("After_1");
    }

    public void Event02_Hold()
    {
        MysterySystem.Instance.EventEnd("After_2");
    }
    #endregion

    #region EVENT03 - Tomb
    public void Event03_Pray()
    {
        MysterySystem.Instance.EventEnd("After_1");
    }

    public void Event03_Excavate()
    {
        MysterySystem.Instance.EventEnd("After_2");
    }

    public void Event03_Leave()
    {
        MysterySystem.Instance.EventEnd("After_1");
    }
    #endregion

    #region EVENT04 - Mask Man
    public void Event04_Deal_Red()
    {
        MysterySystem.Instance.EventEnd("After_Red");

        if (Player.Instance == null) return;

        Player.Instance.ATK.AddModifier(new StatModifier(1, StatModType.Int, this));
    }

    public void Event04_Deal_Blue()
    {
        MysterySystem.Instance.EventEnd("After_Blue");

        if (Player.Instance == null) return;

        Player.Instance.DEF.AddModifier(new StatModifier(1, StatModType.Int, this));
    }

    public void Event04_Deal_Green()
    {
        MysterySystem.Instance.EventEnd("After_Green");

        if (Player.Instance == null) return;

        Player.Instance.ACT.AddModifier(new StatModifier(3, StatModType.Int, this));
    }

    public void Event04_Ignore()
    {
        MysterySystem.Instance.EventEnd("After_Ignored");
    }
    #endregion

    #region EVENT05 - Blacksmith
    public void Event05_Smith()
    {
        MysterySystem.Instance.EventEnd("After");
    }
    #endregion
}
