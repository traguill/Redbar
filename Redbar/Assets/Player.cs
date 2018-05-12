using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Piece {

    private float distanceMove = 0.0f;
	// Use this for initialization
	void Start () {
        distanceMove = 4;//GameManager.instance.cellSize;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Move(GameEnums.Directions.UP);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Move(GameEnums.Directions.DOWN);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(GameEnums.Directions.LEFT);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(GameEnums.Directions.RIGHT);
        }

    }

    void Move(GameEnums.Directions direction)
    {
        switch(direction)
        {
            case GameEnums.Directions.UP:
                gameObject.transform.position += new Vector3(0.0f, distanceMove, 0.0f);
                break;
            case GameEnums.Directions.DOWN:
                gameObject.transform.position -= new Vector3(0.0f, distanceMove, 0.0f);
                break;
            case GameEnums.Directions.RIGHT:
                gameObject.transform.position += new Vector3(distanceMove, 0.0f, 0.0f);
                break;
            case GameEnums.Directions.LEFT:
                gameObject.transform.position -= new Vector3(distanceMove, 0.0f, 0.0f);
                break;
            
        }
    }
}
