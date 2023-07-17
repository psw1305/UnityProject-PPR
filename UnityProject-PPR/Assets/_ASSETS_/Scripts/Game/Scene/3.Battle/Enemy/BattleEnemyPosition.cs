using UnityEngine;

public static class BattleEnemyPosition
{
    public static void SetPosition(this BattleEnemy battleEnemy, int maxSize, int num)
    {
        float rangeX;

        if (maxSize == 3) rangeX = 83.0f;
        else if (maxSize == 2) rangeX = 50.0f;
        else rangeX = 0;

        battleEnemy.transform.localPosition = new Vector3(rangeX * num, 0, 0);
    }
}
