using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
	public float woodCount;
	public float stoneCount;
	public float ironCount;
	//May end up moving fame and health to their own scripts.
	public float fameCount;
	public float healthCount;

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

	void OnCollisionEnter2D(Collision2D col){
				if (col.gameObject.tag == "Enemy") {
						healthCount -= 5;
				}
		}
	void OnTriggerStay2D (Collider2D col){
		//Debug.Log ("PlayerResources.OnCollisionEnter2D");
		//add a keypress or gather action of some sort to this
		if (col.gameObject.tag == "tree" && Input.GetKeyDown ("g") ) {
			woodCount += 5;
			Debug.Log ("wood collected");
		}
	}
}
