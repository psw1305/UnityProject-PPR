using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BattleEnemySkill : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EnemySkillType skillType;
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI skillValueText;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Value")]
    [SerializeField] private int skillValue;
    [SerializeField] private AudioClip[] skillSFX;

    private void Awake()
    {
        this.canvasGroup.alpha = 0.0f;
    }

    public virtual void Enable(BattleEnemy battleEnemy)
    {
        switch (this.skillType)
        {
            case EnemySkillType.Attack:
                int attackValue = this.skillValue + battleEnemy.CurrentAP;
                this.skillValueText.text = attackValue.ToString();
                break;
            case EnemySkillType.Defense:
                this.skillValueText.text = this.skillValue.ToString();
                break;
            case EnemySkillType.Buff:
                this.skillValueText.text = "";
                break;
        }
        
        this.canvasGroup.DOFade(1, 0.3f);
    }

    public virtual IEnumerator Use(BattleEnemy battleEnemy)
    {
        SkillType(battleEnemy);

        yield return YieldCache.WaitForSeconds(0.3f);

        Disable();
    }

    protected void SkillType(BattleEnemy battleEnemy)
    {
        if (this.skillSFX != null) BattleSFX.Instance.Play(this.skillSFX);

        switch (this.skillType)
        {
            case EnemySkillType.Attack:
                BattlePlayer.Instance.Damaged(this.skillValue + battleEnemy.CurrentAP);
                break;
            case EnemySkillType.Defense:
                battleEnemy.ShieldPoint(this.skillValue);
                break;
            case EnemySkillType.Buff:
                battleEnemy.CurrentAP += this.skillValue;
                break;
        }
    }

    public void Disable()
    {
        this.canvasGroup
            .DOFade(0, 0.3f)
            .OnComplete(() => { Destroy(this); });
    }
}
