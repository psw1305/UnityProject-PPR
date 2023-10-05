using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀링
/// https://www.youtube.com/watch?v=fsDE_mO4RZM&t=564s
/// </summary>
public class ObjectPool
{
    private PoolableObject prefab;
    private List<PoolableObject> availableObjects;

    private ObjectPool(PoolableObject prefab, int size)
    {
        this.prefab = prefab;
        this.availableObjects = new List<PoolableObject>(size);
    }

    public static ObjectPool CreateInstance(PoolableObject prefab, Transform parent, int size)
    {
        var pool = new ObjectPool(prefab, size);
        pool.CreateObjects(parent, size);

        return pool;
    }

    private void CreateObjects(Transform parent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            var poolableObject = GameObject.Instantiate(this.prefab, parent);
            poolableObject.parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }

    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        this.availableObjects.Add(poolableObject);
    }

    public PoolableObject GetObject()
    {
        if (this.availableObjects.Count > 0)
        {
            var instance = this.availableObjects[0]; 
            this.availableObjects.RemoveAt(0);
            instance.gameObject.SetActive(true);

            return instance;
        }

        throw new System.Exception($"Could not get an object from pool \"{this.prefab.name}\" Pool");
    }
}
