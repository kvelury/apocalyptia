using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
	public float woodCount;
	public float stoneCount;
	public float ironCount;
	//May end up moving fame and health to their own scripts.
	public float fameCount;
	public float healthCount;

	//Once we know how we're doing resource tiles, we can fill out this method.
	void CollectResources(){
	}

	// Use this for initialization
	void Start () {
		healthCount = 100;
		woodCount = fameCount = stoneCount = ironCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (healthCount <= 0)
						Application.LoadLevel ("DeathScene");
	
	}
}
