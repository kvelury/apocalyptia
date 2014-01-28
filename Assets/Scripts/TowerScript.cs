using UnityEngine;
using System.Collections;

public class TowerScript : MonoBehaviour {
	
	public const int cost = 25;
	
	//timer to attack in intervals
	public float timer = 0;
	public bool attack = false;
	//public GameObject Weapon;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 swingDir = new Vector3(transform.position.x,transform.position.y, 0);
		Quaternion swingRot = new Quaternion(0,0,0,0);
		
		if (timer <= 0) {
			timer = 5;
			/*
                        //in progress...
                        swingDir.x = transform.position.x + 0.55F;
                        swingDir.y = transform.position.y + 0.55F;

                        swingRot = Quaternion.LookRotation (swingDir - transform.position);
                        swingRot.z = swingRot.w = 0;
                        if(swingDir.x == transform.position.x){
                                swingRot.y = swingRot.x;
                        }
                        if(swingDir.x != transform.position.x || swingDir.y != transform.position.y){
                                //we need account for the isometric rotation
                                GameObject swingInstance = Instantiate (Weapon,swingDir, swingRot) as GameObject;
                        }
                        */
		} else {
			timer -= Time.deltaTime;
		}
	}
	
}