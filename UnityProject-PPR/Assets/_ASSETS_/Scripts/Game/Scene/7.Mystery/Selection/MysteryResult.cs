using PSW.Core.Stat;
using UnityEngine;

public class MysteryResult : MonoBehaviour
{
    #region EVENT01
    public void Event01_PlusMaxHP()
    {
        UISFX.Instance.Play(UISFX.Instance.healthUp);

        if (Player.Instance == null) return;

        Player.Instance.HP.AddModifier(new StatModifier(8, StatModType.Int, this));
        Player.Instance.SetHP(Player.Instance.CurrentHP + 8);
    }

    public void Event01_ObtainRelic()
    {
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        // 일반 등급 유물
        Player.Instance.AddItemRelic(1);
    }

    public void Event01_ObtainPotions()
    {
        UISFX.Instance.Play(UISFX.Instance.itemGain);

        if (Player.Instance == null) return;

        for (int i = 0; i < 3; i++)
        {
            Player.Instance.AddItemPotion();
        }
    }

    public void Event01_ReceiveGold()
    {
        UISFX.Instance.Play(UISFX.Instance.cashGain);

        if (Player.Instance == null) return;

        Player.Instance.SetCash(100);
    }
    #endregion
}
