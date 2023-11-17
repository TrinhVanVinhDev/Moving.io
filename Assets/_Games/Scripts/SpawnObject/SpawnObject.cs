using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : Singleton<SpawnObject>
{
    [SerializeField] private List<PropertyPool> pools;
    [SerializeField] private Transform planeTranformA;
    [SerializeField] private Transform planeTranformB;
    [SerializeField] private Transform planeTranformC;

    private List<Vector3> listPosition = new List<Vector3>();
    [HideInInspector] public GameObject objEnemy;
    [HideInInspector] public int sizePool;
    [HideInInspector] public string tagPool;

    protected Dictionary<string, Queue<GameObject>> dictionaryPool;

    public void OnInit()
    {
        dictionaryPool = new Dictionary<string, Queue<GameObject>>();

        foreach (PropertyPool pool in pools)
        {
            sizePool = pool.size;
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                objEnemy = Instantiate(pool.enemyPrefabs);
                objEnemy.transform.SetParent(pool.parentObject.transform);
                objEnemy.SetActive(false);
                objectPool.Enqueue(objEnemy);
            }

            dictionaryPool.Add(pool.tag, objectPool);
        }
    }

    public GameObject OnSpawnObject(string tag)
    {
        if (!dictionaryPool.ContainsKey(tag))
        {
            Debug.LogWarning("is tag: " + tag + " not exist");
            return null;
        }

        GameObject objectToSpawn = dictionaryPool[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = CreateRandomPosition();
        objectToSpawn.transform.rotation = Quaternion.identity;
        dictionaryPool[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    private Vector3 CreateRandomPosition()
    {

        float xPosition = Random.Range(planeTranformA.position.x, planeTranformB.position.x);
        float zPosition = Random.Range(planeTranformC.position.z, planeTranformB.position.z);

        Vector3 position = new Vector3(xPosition, 0f, zPosition);
        if (listPosition.Count > 49)
        {
            listPosition = new List<Vector3>();
        }
        if (!listPosition.Contains(position))
        {
            listPosition.Add(position);
        }
        else
        {
            CreateRandomPosition();
        }
        return position;
    }
}
