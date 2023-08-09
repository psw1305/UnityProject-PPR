using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyEncounter", menuName = "Blueprint/Encounter")]
public class EnemyEncounter : ScriptableObject
{
    [Header("Spawn Enemy")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyBlueprint[] spawnEnemys;

    public EnemyType EnemyType => this.enemyType;
    public EnemyBlueprint[] SpawnEnemys => this.spawnEnemys;
    public int SpawnCount => spawnEnemys.Length;
}
