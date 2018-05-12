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
    public Vector2[] lvlShape;

    [Header("Debug")]
    public bool showLevel = false;

    int[,] gridDebug;

    void OnValidate()
    {
        gridDebug = new int[gridSize,gridSize];

        for(int i = 0; i < gridSize; i++)
            for(int j = 0; j < gridSize; j++)
            {
                gridDebug[i, j] = 0;
            }

        for(int i = 0; i < lvlShape.Length; i++)
        {
            int x = (int)lvlShape[i].x;
            int y = (int)lvlShape[i].y;

            gridDebug[x, y] = 1;
        }
    }

	// Use this for initialization
	void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        if (showLevel == false)
            return;
        for (int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
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
        }
    }
}