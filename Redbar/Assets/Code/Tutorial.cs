using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    // Interactions to happen
    bool event_carNoInteract = false;       // 0
    bool event_carInteract = false;         // 1
    bool event_peopleNoInteract1 = false;   // 2
    bool event_peopleNoInteract2 = false;   // 3
    bool event_peopleInteract = false;      // 4

    int actionsCompleted = 0; // To 5
    public bool done = false;

    [HideInInspector]
    public static Tutorial g_tutorial;

    void Awake()
    {
        g_tutorial = this;
    }
    
    public void LaunchTutorialEvent()
    {
        int[] rndPool = new int[5 - actionsCompleted];

        if (event_carNoInteract) rndPool[rndPool.Length] = 0;
        if (event_carInteract) rndPool[rndPool.Length] = 1;
        if (event_peopleNoInteract1) rndPool[rndPool.Length] = 2;
        if (event_peopleNoInteract2) rndPool[rndPool.Length] = 3;
        if (event_peopleInteract) rndPool[rndPool.Length] = 4;

        int eventId = Random.Range(0, rndPool.Length - 1);

        StartTutorialEvent(rndPool[eventId]);
    }

    private void StartTutorialEvent(int id)
    {
        switch(id)
        {
            case 0: // Car no interact
                event_carNoInteract = true;
                Enemymanager.g_EnemyManager.SpawnCar(false);
                break;
            case 1: // Car interact
                event_carInteract = true;
                Enemymanager.g_EnemyManager.SpawnCar(true);
                break;
            case 2: // people no interact 1
                event_peopleNoInteract1 = true;
                Enemymanager.g_EnemyManager.SpawnDrunk(false);
                break;
            case 3: // peopole no interact 2
                event_peopleNoInteract2 = true;
                Enemymanager.g_EnemyManager.SpawnSilhouette(false);
                break;
            case 4: // people interact
                event_peopleInteract = true;
                Enemymanager.g_EnemyManager.SpawnGroup(true);
                break;
        }

        actionsCompleted++;
        if (actionsCompleted == 5)
            Game_Manager.g_GameManager.isTutorialFinished = true;
    }

}
