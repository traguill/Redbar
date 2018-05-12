using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour 
{
    [Header("Set up")]
    public Sprite cellSprite;
    [Range(1, 60)]
    public int gridSize;
    public int cellSize;
    [Header("The shape to form")]
    public Vector2[] lvlShape;
    [Header("Initial position of other pieces")]
    public PieceData[] lvlPieces;

    [Header("Debug")]
    public bool showFinalShape = false;
    public bool showPiecesLoc = false;

    int[,] grid; // The good one
    int[,] gridState; // The current state of the grid
    int[,] gridDebug; // Debug
    int[,] gridStateDebug; // The current state of the grid Debug


    void OnValidate()
    {
        gridStateDebug = new int[gridSize, gridSize];
        gridDebug = new int[gridSize,gridSize];

        for(int i = 0; i < gridSize; i++)
            for(int j = 0; j < gridSize; j++)
            {
                gridDebug[i, j] = 0;
                gridStateDebug[i, j] = 0;
            }

        for(int i = 0; i < lvlShape.Length; i++)
        {
            int x = (int)lvlShape[i].x;
            int y = (int)lvlShape[i].y;

            gridDebug[x, y] = -1;
        }

        for (int i = 0; i < lvlPieces.Length; i++)
        {
            gridStateDebug[lvlPieces[i].x, lvlPieces[i].y] = lvlPieces[i].type;
        }
    }

	// Use this for initialization
	void Start () {
        InitializeGrid();		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool CheckResolveGame()
    {
        for(int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; ++y)
            {
                if(grid[x,y] != 0)
                {
                    if (gridState[x, y] == 0)
                        return false;
                }
            }
        }

        return true;
    }

    private void InitializeGrid()
    {
        grid = new int[gridSize, gridSize];

        // Initialize the grid to 0 - Empty

        for (int i = 0; i < gridSize; i++)
            for (int j = 0; j < gridSize; j++)
                grid[i, j] = 0;
         
        // Set the other pieces value in the grid
        for(int i = 0; i < lvlPieces.Length; i++) 
        {
            grid[lvlPieces[i].x, lvlPieces[i].y] = lvlPieces[i].type;
        }

    }

    // Debug --------------------------------------------------------------------------------------------------

    void OnDrawGizmos()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                if(showFinalShape)
                {
                    if (gridDebug[x, y] == 0)
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawWireCube(new Vector3(x * cellSize, y * cellSize, 0), new Vector3(cellSize, cellSize, 0.00001f));
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawCube(new Vector3(x * cellSize, y * cellSize, 0), new Vector3(cellSize, cellSize, 0.00001f));
                    }
                }
               
                
                if(showPiecesLoc && gridStateDebug[x, y] != 0)
                {
                    Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.4f);
                    Gizmos.DrawCube(new Vector3(x * cellSize, y * cellSize, 0), new Vector3(cellSize, cellSize, 0.00001f));
                }
            }
        }
    }
}