using UnityEngine;
using System.Collections;

public class PlayerResources : MonoBehaviour {
	//*************************************************
	//These will remain null, but are needed for the 
	//creation operation
	public GameObject woodenHouse;
	//*************************************************
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
		//Debug.Log (PlayerPrefs.GetFloat("Fame").ToString());
	}
	
	// Update is called once per frame
	void Update () {
		if (healthCount <= 0) {
			//Debug.Log ("Fame: " + fameCount.ToString());
			PlayerPrefs.SetFloat("Fame", fameCount);
			//put stuff here about moving world data into player preferences
			//this will work because this is the last thing that is seen before the 
			//scene moves on
			float[] inventory = GameObject.Find("Player").GetComponent<PlayerStats>().inv;
			for (int i=0; i<inventory.Length; i++){
				PlayerPrefs.SetFloat("Inventory " + i.ToString(), inventory[i]);
			}
			PlayerPrefs.Save ();
			Application.LoadLevel ("DeathScene");
		}

		//place a building
		if(Input.GetKey ("b") && this.woodCount >= WoodenHouseScript.cost && Input.GetKeyDown ("b")){
			this.woodCount -= WoodenHouseScript.cost;
			Vector3 v = new Vector3(this.transform.localPosition.x,
			                        this.transform.localPosition.y,
			                        0);
			GameObject newWoodenHouse = Instantiate (woodenHouse, v, new Quaternion(0, 0, 0, 0)) as GameObject;
		}
		//PlayerPrefs.SetFloat("Fame", fameCount);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Enemy") {
			healthCount -= 5;
		}
	}

	void OnTriggerStay2D (Collider2D col){
		//add a keypress or gather action of some sort to this
		if (col.gameObject.tag == "tree" && Input.GetKeyDown ("g")) {
			woodCount += 5;
		}else if (col.gameObject.tag == "stone" && Input.GetKeyDown ("g")) {
			stoneCount += 5;
		}else if (col.gameObject.tag == "NPC" && Input.GetKeyDown ("t")) {
			Debug.Log ("NPC Greeting!");
			//Dialogue script call goes here!
		}

	}

	void OnDestroy(){
		//PlayerPrefs.DeleteKey("Fame");
		//Debug.Log ("Fame: " + fameCount.ToString());
		PlayerPrefs.SetFloat("Fame", fameCount);
		PlayerPrefs.Save ();
	}
}
