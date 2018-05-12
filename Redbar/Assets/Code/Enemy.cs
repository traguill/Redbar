using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    DRUNK,
    GROUP,
    CAR,
    SILHOUETTE,
    NONE
}

public class Enemy : MonoBehaviour {

    [SerializeField] public bool reacts = false;
    [SerializeField] public EnemyType type;
    [SerializeField] public int reactionProbability;

	// Use this for initialization
	void Start () {
		
	}

    public void Init(EnemyType enemy, int reactionProb)
    {
        reactionProbability = reactionProb;

        int reaction = Random.Range(0, 100);
        if (reaction <= reactionProbability)
        {
            reacts = true;
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
