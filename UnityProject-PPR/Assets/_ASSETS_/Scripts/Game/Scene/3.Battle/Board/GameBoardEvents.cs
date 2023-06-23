using UnityEngine.Events;

public class InitializeEvent : UnityEvent { }
public class IntCountEvent : UnityEvent<int> { }
public class StatPointChanged : UnityEvent<int, int> { }

public static class GameBoardEvents
{
    /// <summary>
    /// �÷��̾� Turn �� ���
    /// </summary>
    public static InitializeEvent OnPlayerTurnInit { get; } = new InitializeEvent();

    /// <summary>
    /// �� Turn �� ���
    /// </summary>
    public static InitializeEvent OnEnemyTurnInit { get; } = new InitializeEvent();

    /// <summary>
    /// elements�� despawned �� ��
    /// </summary>
    public static InitializeEvent OnElementsDespawned { get; } = new InitializeEvent();

    /// <summary>
    /// elements�� selection�� �ٲ� ��
    /// </summary>
    public static IntCountEvent OnSelectionChanged { get; } = new IntCountEvent();

    /// <summary>
    /// �÷��̾� ü���� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnPlayerHealthChanged { get; } = new StatPointChanged();

    /// <summary>
    /// �÷��̾� ��ȭ ��ġ�� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnPlayerCashChanged { get; } = new StatPointChanged();

    /// <summary>
    /// �÷��̾� ���ݷ��� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnPlayerAttackChanged { get; } = new StatPointChanged();

    /// <summary> 
    /// �÷��̾� ���� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnPlayerShieldChanged { get; } = new StatPointChanged();

    /// <summary>
    /// �� ü���� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnEnemyHealthChanged { get; } = new StatPointChanged();

    /// <summary> 
    /// �� ���� �ٲ� ��
    /// </summary>
    public static StatPointChanged OnEnemyShieldChanged { get; } = new StatPointChanged();
}
