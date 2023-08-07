using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleEnemySkill : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private EnemySkill skillType;
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

    public void Enable(BattleEnemy battleEnemy)
    {
        switch (this.skillType)
        {
            case EnemySkill.Attack:
                int attackValue = this.skillValue + battleEnemy.CurrentAP;
                this.skillValueText.text = attackValue.ToString();
                break;
            case EnemySkill.Defense:
                this.skillValueText.text = this.skillValue.ToString();
                break;
            case EnemySkill.Reinforce:
                this.skillValueText.text = "";
                break;
        }
        
        this.canvasGroup.DOFade(1, 0.3f);
    }

    public void Use(BattleEnemy battleEnemy)
    {
        if (this.skillSFX != null) BattleSFX.Instance.Play(this.skillSFX);

        switch (this.skillType) 
        {
            case EnemySkill.Attack:
                BattlePlayer.Instance.Damaged(this.skillValue + battleEnemy.CurrentAP);
                break;
            case EnemySkill.Defense:
                battleEnemy.ShieldPoint(this.skillValue);
                break;
            case EnemySkill.Reinforce:
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
