using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    // Use this for initialization
    private GameEnums.PieceColor color;
    private GameEnums.PieceShape shape;

    private SpriteRenderer shape_renderer;

    public void Init(GameEnums.PieceColor piece_col, GameEnums.PieceShape piece_shape)
    {
        color = piece_col;
        shape = piece_shape;

        shape_renderer = GetComponent<SpriteRenderer>();
        switch (color)
        {
            case GameEnums.PieceColor.BLUE:
                shape_renderer.material.color = Color.blue;
                break;

            case GameEnums.PieceColor.RED:
                shape_renderer.material.color = Color.red;
                break;
        }
    }

    void Start () {
        
        
	}
	
	// Update is called once per frame
	private void Update () {

	}
}
