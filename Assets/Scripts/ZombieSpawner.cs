using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public static ZombieSpawner Instance;
    public Zombie zombiePrefab; // 좀비 원본
    
    private List<Zombie> zombies = new List<Zombie>();  //  좀비를 담는 리스트
    public Transform[] spawnPoints; //좀비 스폰 포인트
    public ZombieData[] zombieDatas;
    private int wave;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void Update()
    {
        if(GameManager.instance !=null &&GameManager.instance.isGameover)
        {
            return;
        }
        if(zombies.Count == 0)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        wave++;
        int SpawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i = 0; i<SpawnCount; i++)
        {
            SpawnZombie();
        }
        UpdateUI();
    }
    

    private void UpdateUI()
    {
        UIManager.instance.UpdateWaveText(wave, zombies.Count);
    }

    private void SpawnZombie()
    {

        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position,spawnPoint.rotation);

        zombie.Setup(zombieData);
        zombies.Add(zombie);

        zombie.onDeath += () => zombies.Remove(zombie);
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        zombie.onDeath += () => GameManager.instance.AddScore(zombieData.score) ;
    }
  
    // Update is called once per frame
    
}
