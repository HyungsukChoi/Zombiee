using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MyItemSpawner : MonoBehaviour
{
    public Transform playerTr;
    public GameObject[] items;
    float maxDistance = 5f;
    public float timeBetspawnMax = 3f;
    public float timeBetspawnMin = 1f;
    public float timeBetspawn;
    public float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        timeBetspawn = Random.Range(timeBetspawnMin, timeBetspawnMax);
        lastSpawnTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= lastSpawnTime + timeBetspawn && playerTr != null)
        {
            lastSpawnTime = Time.time;
            timeBetspawn = Random.Range(timeBetspawnMin, timeBetspawnMax);
            SpawnItem();
        }
    }
    
    void SpawnItem()
    {
        GameObject item = items[Random.Range(0, items.Length)];
        Instantiate(item, RandomPositionOnNavmesh(playerTr.position, maxDistance)+Vector3.up*0.5f, Quaternion.identity);
    }

    Vector3 RandomPositionOnNavmesh(Vector3 center, float distance)
    {
        Vector3 RandomPos = center + Random.insideUnitSphere * distance;
        NavMeshHit hit;
        NavMesh.SamplePosition(RandomPos, out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }
}
