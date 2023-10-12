using PSW.Core.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BattleEnemySkill : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected EnemySkillType skillType;
    [SerializeField] protected Image skillIcon;
    [SerializeField] protected TextMeshProUGUI skillValueText;

    [Header("Param")]
    [SerializeField] protected int skillValue;
    [SerializeField] protected AudioClip[] skillSFX;

    protected BattleEnemy battleEnemy;
    protected GameBoard gameBoard;
    protected CanvasGroup canvasGroup;
    protected int resultSkillValue;

    private void Awake()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.canvasGroup.alpha = 0.0f;
    }

    public void Set(BattleEnemy battleEnemy, GameBoard gameBoard)
    {
        this.battleEnemy = battleEnemy;
        this.gameBoard = gameBoard;
        this.resultSkillValue = this.skillValue;

        CheckSkill();

        this.canvasGroup.DOFade(1, 0.3f);
    }

    protected void UseSkillByType()
    {
        if (this.skillSFX != null) BattleSFX.Instance.Play(this.skillSFX);

        switch (this.skillType)
        {
            case EnemySkillType.Attack:
                BattlePlayer.Instance.Damaged(this.resultSkillValue);
                break;
            case EnemySkillType.Defense:
                this.battleEnemy.ShieldPoint(this.resultSkillValue);
                break;
            case EnemySkillType.Buff:
                this.battleEnemy.CurrentAP += this.resultSkillValue;
                break;
        }
    }

    /// <summary>
    /// 적 스킬 사용
    /// </summary>
    public virtual IEnumerator UseSkill()
    {
        UseSkillByType();

        yield return YieldCache.WaitForSeconds(0.3f);

        Disable();
    }

    /// <summary>
    /// 적 상태 체크
    /// </summary>
    public virtual void CheckSkill() 
    {
        this.skillValueText.text = this.resultSkillValue.ToString();
    }

    /// <summary>
    /// 스킬 비활성화
    /// </summary>
    public void Disable()
    {
        this.canvasGroup
            .DOFade(0, 0.3f)
            .OnComplete(() => { Destroy(this); });
    }
}
