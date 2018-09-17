using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {


    public MeshFilter go_floor;
    public MeshFilter go_wall;

    public GameObject portal;
    public GameObject lamp;

    public Sprite winPortal;

    Mesh floor;
    Mesh wall;

    public GameObject[] decorationList;
    GameObject currentDec = null;

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
            if (currentDec != null)
                currentDec.SetActive(false);
            int rndID = Random.Range(0, decorationList.Length + 4);
            int rndLamp = Random.Range(0, 100);
            if (rndLamp < 70)
                lamp.SetActive(true);
            else
                lamp.SetActive(false);
            if(rndID == decorationList.Length)
            {
                currentDec = portal;
                currentDec.SetActive(true);
                if(EnemyManager.g_EnemyManager.win)
                {
                    currentDec.GetComponent<Portal>().SetHome();
                    portal.gameObject.GetComponent<SpriteRenderer>().sprite = winPortal;
                }

                lamp.SetActive(true);
            }else
            if(rndID < decorationList.Length)
            {
                currentDec = decorationList[rndID];
                currentDec.SetActive(true);
            }
            this.transform.position = new Vector3(transform.position.x + (go_wall.mesh.bounds.size.x * 5), transform.position.y, transform.position.z);
        }        
       

	}


    //Unused function
    bool IsOutOfCamera(ref Plane[] planes, Mesh s)
    {
        return !GeometryUtility.TestPlanesAABB(planes, s.bounds);
    }

}
