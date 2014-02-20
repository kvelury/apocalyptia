/*********************************************************
 * This class serves as the base class for everything that
 * can be edited or interacted with in the terrain.  It is
 * minimal right now but will be filled out as we decide
 * exactly how we want the player to interact.  For now,
 * it serves largely to be able to organize all tile 
 * objects in one location
 * *******************************************************/


using UnityEngine;
using System.Collections;

public class TileObject : MonoBehaviour {

	public enum resourceType:byte{Trees, Rocks, Iron, Building};

	public byte type;
	public int amount;

	// Use this for initialization
	public virtual void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	public virtual void initialize(byte type, int amount){
		this.type = type;
		this.amount = amount;
	}
}
