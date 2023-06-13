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
                // 위치 초기화
                this.transform.localPosition = Vector3.zero;
            })
            .Append
            (
                // 화면 밖 -> 안
                this.transform.DOLocalMoveX(300, 0.4f).SetEase(Ease.OutSine)
            )
            .AppendInterval(1.0f) // delay
            .Append
            (
                // 화면 안 -> 밖
                this.transform.DOLocalMoveX(600, 0.4f).SetEase(Ease.OutSine)
            )
            .AppendInterval(0.25f); // delay

        this.sequence.SetAutoKill(false);
        this.sequence.Pause();
    }

    /// <summary>
    /// 턴이 바뀔때 마다 notice update
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
