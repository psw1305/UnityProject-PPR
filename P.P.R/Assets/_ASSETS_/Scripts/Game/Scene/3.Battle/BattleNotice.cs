using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattleNotice : BehaviourSingleton<BattleNotice>
{
    [SerializeField] private TextMeshProUGUI noticeText;
    private Sequence sequence;

    protected override void Awake()
    {
        base.Awake();

        SetSequence();
    }

    /// <summary>
    /// doTween sequence setup
    /// </summary>
    private void SetSequence()
    {
        // sequence setup
        this.sequence = DOTween.Sequence()
            .OnStart(() =>
            {
                // ��ġ �ʱ�ȭ
                this.transform.localPosition = Vector3.zero;
            })
            .Append
            (
                // ȭ�� �� -> ��
                this.transform.DOLocalMoveX(300, 0.4f).SetEase(Ease.OutSine)
            )
            .AppendInterval(1.0f) // delay
            .Append
            (
                // ȭ�� �� -> ��
                this.transform.DOLocalMoveX(600, 0.4f).SetEase(Ease.OutSine)
            )
            .AppendInterval(0.25f); // delay

        this.sequence.SetAutoKill(false);
        this.sequence.Pause();
    }

    /// <summary>
    /// ���� �ٲ� ���� notice update
    /// </summary>
    /// <param name="text"></param>
    public IEnumerator UpdateNotice(string text)
    {
        StopAllCoroutines();

        this.noticeText.text = text;
        this.sequence.Restart();
        yield return this.sequence.WaitForCompletion();
    }
}
