using UnityEngine.Events;

public class InitializeEvent : UnityEvent { }
public class IntCountEvent : UnityEvent<int> { }
public class StatPointChanged : UnityEvent<int, int> { }

public static class GameBoardEvents
{
    /// <summary>
    /// 플레이어 Turn 일 경우
    /// </summary>
    public static InitializeEvent OnPlayerTurnInit { get; } = new InitializeEvent();

    /// <summary>
    /// 적 Turn 일 경우
    /// </summary>
    public static InitializeEvent OnEnemyTurnInit { get; } = new InitializeEvent();

    /// <summary>
    /// elements가 despawned 일 때
    /// </summary>
    public static InitializeEvent OnElementsDespawned { get; } = new InitializeEvent();

    /// <summary>
    /// elements의 selection이 바뀔 때
    /// </summary>
    public static IntCountEvent OnSelectionChanged { get; } = new IntCountEvent();

    /// <summary>
    /// 플레이어 체력이 바뀔 때
    /// </summary>
    public static StatPointChanged OnPlayerHealthChanged { get; } = new StatPointChanged();

    /// <summary>
    /// 플레이어 재화 수치가 바뀔 때
    /// </summary>
    public static StatPointChanged OnPlayerCashChanged { get; } = new StatPointChanged();

    /// <summary>
    /// 플레이어 공격력이 바뀔 때
    /// </summary>
    public static StatPointChanged OnPlayerAttackChanged { get; } = new StatPointChanged();

    /// <summary> 
    /// 플레이어 방어력 바뀔 때
    /// </summary>
    public static StatPointChanged OnPlayerShieldChanged { get; } = new StatPointChanged();

    /// <summary>
    /// 적 체력이 바뀔 때
    /// </summary>
    public static StatPointChanged OnEnemyHealthChanged { get; } = new StatPointChanged();

    /// <summary> 
    /// 적 방어력 바뀔 때
    /// </summary>
    public static StatPointChanged OnEnemyShieldChanged { get; } = new StatPointChanged();
}
