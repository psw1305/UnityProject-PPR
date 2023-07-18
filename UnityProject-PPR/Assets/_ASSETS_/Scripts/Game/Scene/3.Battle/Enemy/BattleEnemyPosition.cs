using UnityEngine;

public static class BattleEnemyPosition
{
    public static void SetPosition(this BattleEnemy battleEnemy, int maxSize, int num)
    {
        if (maxSize == 3) 
            battleEnemy.transform.localPosition = new Vector3(85.0f * (num - 1), 0, 0);
        else if (maxSize == 2)
            battleEnemy.transform.localPosition = new Vector3(-50.0f + (num * 50.0f * 2), 0, 0);
        else
            battleEnemy.transform.localPosition = new Vector3(0, 0, 0);
    }
}
