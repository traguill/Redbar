using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {


    [SerializeField] public GameObject player;
    [SerializeField] public GridManager gridManager;

    public float cellSize = 0.0f;
    public int gridSize = 0;

    public Game_Manager instance;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {

        player.GetComponent<Player>().Init(GameEnums.PieceColor.RED, GameEnums.PieceShape.PIECE_T);
        cellSize = gridManager.GetComponent<GridManager>().cellSize;
        gridSize = gridManager.GetComponent<GridManager>().gridSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
