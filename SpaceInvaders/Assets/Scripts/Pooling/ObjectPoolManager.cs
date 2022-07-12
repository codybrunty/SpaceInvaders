using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>{
    [NonReorderable] [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> dictPool = new Dictionary<string, Queue<GameObject>>();

    private void Start() {
        InstantiatePools();
    }

    private void InstantiatePools() {
        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab, gameObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            dictPool.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!dictPool.ContainsKey(tag)) {
            Debug.Log("Pool tag(" + tag + ") doesn't exist in Pool Dictionary");
            return null;
        }
        GameObject objToSpawn = dictPool[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        dictPool[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

}
