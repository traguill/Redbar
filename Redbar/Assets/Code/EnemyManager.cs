using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymanager : MonoBehaviour 
{
    [Header("Probability to spawn")]
    public int drunkSpawnPer = 25;
    public int carSpawnPer = 25;
    public int groupSpawnPer = 25;
    public int silhouetteSpawnPer = 25;

    [Header("Probability to interact")]
    public int drunkIntPer = 70;
    public int carIntPer = 50;
    public int groupIntPer = 60;
    public int silhouetteIntPer = 50;

    [HideInInspector]
    public int numTotalEvents = 0;

    [Header("Num interactions to finish")]
    public int numToEndEvents = 10;

    [Header("Enemies: To Set Up")]
    public Enemy drunk;
    public Enemy car;
    public Enemy group;
    public Enemy silhouette;

    Enemy currentEnemy = null;
    public Vector3 spawnLocationRight;
    public Vector3 spawnLocationLeft;

    enum Enemies
    {
        DRUNK,
        CAR,
        SILHOUETTE,
        GROUP
    }

    [HideInInspector]
    public static Enemymanager g_EnemyManager;

    Enemies[] enemyRandomizer;
    
    void Awake()
    {
        g_EnemyManager = this;

        enemyRandomizer = new Enemies[100];
        for(int i = 0; i < 100; i++)
        {
            if(drunkSpawnPer != 0)
            {
                enemyRandomizer[i] = Enemies.DRUNK;
                drunkSpawnPer--;
                continue;
            }
            if(carSpawnPer != 0)
            {
                enemyRandomizer[i] = Enemies.CAR;
                carSpawnPer--;
                continue;
            }
            if(groupSpawnPer != 0)
            {
                enemyRandomizer[i] = Enemies.GROUP;
                groupSpawnPer--;
                continue;
            }
            if(silhouetteSpawnPer != 0)
            {
                enemyRandomizer[i] = Enemies.SILHOUETTE;
                silhouetteSpawnPer--;
            }
        }

    }

    // Init Enemies if tutorial is not over notify tutorial if not notify itself
    public void SpawnDrunk(bool interact)
    {
        numTotalEvents++;
        SpawnEnemy(drunk, interact);
    }

    public void SpawnCar(bool interact)
    {
        numTotalEvents++;
        SpawnEnemy(car, interact);
    }

    public void SpawnGroup(bool interact)
    {
        numTotalEvents++;
        SpawnEnemy(group, interact);
    }

    public void SpawnSilhouette(bool interact)
    {
        numTotalEvents++;
        SpawnEnemy(silhouette, interact);
    }

    private void SpawnEnemy(Enemy copy, bool interact)
    {
        Vector3 spawnLoc = Random.Range(0, 1) == 0 ? spawnLocationLeft : spawnLocationRight;
        spawnLoc += Game_Manager.g_GameManager.player.transform.position;
        currentEnemy = Instantiate(copy, spawnLoc, Quaternion.identity);
        currentEnemy.Init(interact);
    }

    void SpawnRndEvent()
    {
        int rndEnemyID = Random.Range(0, 99);
        Enemies type = enemyRandomizer[rndEnemyID];

        int rndInteract = Random.Range(0, 99);
        bool interact = false;
        switch (type)
        {
            case Enemies.DRUNK:
                if (rndInteract < drunkIntPer)
                    interact = true;
                SpawnDrunk(interact);
                break;
            case Enemies.CAR:
                if (rndInteract < carIntPer)
                    interact = true;
                SpawnCar(interact);
                break;
            case Enemies.SILHOUETTE:
                if (rndInteract < silhouetteIntPer)
                    interact = true;
                SpawnSilhouette(interact);
                break;
            case Enemies.GROUP:
                if (rndInteract < groupIntPer)
                    interact = true;
                SpawnGroup(interact);
                break;
        }
    }
}
