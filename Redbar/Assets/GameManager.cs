using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    [SerializeField] public GameObject player;

    public float cellSize = 0.0f;

    public GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        player.GetComponent<Player>().Init(GameEnums.PieceColor.RED, GameEnums.PieceShape.SQUARE);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
