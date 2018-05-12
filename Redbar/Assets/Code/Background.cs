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

        Vector3 rightPosition = new Vector3(go_wall.transform.position.x + go_wall.mesh.bounds.size.x / 2, go_wall.transform.position.y, go_wall.transform.position.z);
        if(Camera.main.WorldToViewportPoint(rightPosition).x < 0)
        {
            this.transform.position = new Vector3(transform.position.x + (go_wall.mesh.bounds.size.x * 5), transform.position.y, transform.position.z);
        }        
       

	}


    //Unused function
    bool IsOutOfCamera(ref Plane[] planes, Mesh s)
    {
        return !GeometryUtility.TestPlanesAABB(planes, s.bounds);
    }

}
