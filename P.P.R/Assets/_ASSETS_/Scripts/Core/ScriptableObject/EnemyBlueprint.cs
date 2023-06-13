using PSW.Core.Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Blueprint/Enemy")]
public class EnemyBlueprint : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Sprite image;
    [SerializeField] private string enemyName;
    [SerializeField] private int hp;
    [SerializeField] private GameObject[] skills;

    public EnemyType EnemyType => this.enemyType;
    public Sprite EnemyImage => this.image;
    public string EnemyName => this.enemyName;
    public int HP => this.hp;
    public GameObject[] Skills => this.skills;
}