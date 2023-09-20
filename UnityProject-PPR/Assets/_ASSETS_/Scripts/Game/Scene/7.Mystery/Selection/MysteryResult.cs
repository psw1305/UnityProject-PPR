using PSW.Core.Stat;
using UnityEngine;

public class MysteryResult : MonoBehaviour
{
    #region EVENT01 - Starting
    public void Event01_PlusMaxHP()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.healthUp);

        if (Player.Instance == null) return;

        Player.Instance.HP.AddModifier(new StatModifier(8, StatModType.Int, this));
        Player.Instance.SetHP(Player.Instance.CurrentHP + 8);
    }

    public void Event01_ObtainRelic()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        // 일반 등급 유물
        //Player.Instance.AddItemRelic(1);
    }

    public void Event01_ObtainPotions()
    {
        MysterySystem.Instance.EventEnd("After");
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        //for (int i = 0; i < 3; i++)
        //{
        //    Player.Instance.AddItemPotion(1);
        //}
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
    public void Event04_Deal()
    {
        MysterySystem.Instance.EventEnd("After_1");
    }

    public void Event04_Ignore()
    {
        MysterySystem.Instance.EventEnd("After_2");
    }
    #endregion

    #region EVENT05 - Blacksmith
    public void Event05_Smith()
    {
        MysterySystem.Instance.EventEnd("After");
    }
    #endregion
}
