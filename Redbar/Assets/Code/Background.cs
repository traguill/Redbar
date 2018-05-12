using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {


    public MeshFilter go_floor;
    public MeshFilter go_wall;

    Mesh floor;
    Mesh wall;


	// Use this for initialization
	void Start () {
        floor = go_floor.mesh;
        wall = go_wall.mesh;
	}
	
	// Update is called once per frame
	void Update () {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(IsOutOfCamera(ref planes, floor) && IsOutOfCamera(ref planes, wall))
        {

        }
        
       

	}

    bool IsOutOfCamera(ref Plane[] planes, Mesh s)
    {
        return GeometryUtility.TestPlanesAABB(planes, s.bounds);
    }

}
