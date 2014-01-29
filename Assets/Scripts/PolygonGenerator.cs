/***********************************************************************
 * Terrain generator script
 * By Cody Childers
 * Based of tutorials found at:
 * http://studentgamedev.blogspot.com/2013/08/unity-voxel-tutorial-part-1-generating.html
 * http://studentgamedev.blogspot.com/2013/08/VoxelTutP2.html
 * ********************************************************************/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PolygonGenerator : MonoBehaviour {

	//*******************************************************************************************
	//constants
	protected int gridWidth = 50;
	protected int gridHeight = 50;
	protected const float perlinScale = 1.0f;
	protected const float worldScale = 3.0f;
	//*******************************************************************************************
	//references for resources
	public GameObject tree;
	public GameObject stone;
	//*******************************************************************************************

	//list of vertices to the mesh
	public List<Vector3> newVertices = new List<Vector3>();
	//triangles how to build each section of the mesh
	public List<int> newTriangles = new List<int>();
	//UV list to tell Unity how texture is aligned to polygon
	public List<Vector2> newUV = new List<Vector2>();
	//save final terrain as this mesh
	protected Mesh mesh;

	//variables to pick apart the sprite sheet
	protected const float tUnit = 0.25f; //percentage of the width of the image of one tile
    protected readonly Vector2 Grass    = new Vector2(0, 2);
	protected readonly Vector2 DryGrass = new Vector2(0, 1);
	protected readonly Vector2 Stone    = new Vector2(3, 2);
	protected readonly Vector2 Water    = new Vector2(2, 3);
	protected readonly Vector2 Sand     = new Vector2(2, 2);
	protected readonly Vector2 Dirt     = new Vector2(1, 1);
	public enum TileCodes : byte {Grass, DryGrass, Stone, Water, Sand, Dirt};

	protected int squareCount = 0;

	public byte[,] blocks;
	public TileObject[,] tileObjects;
	public GameObject player;


	// Use this for initialization
	public virtual void Start () {
		mesh = GetComponent<MeshFilter>().mesh;

		GenTerrain ();
		BuildMesh ();
		UpdateMesh ();
		AddResources ();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		/*
		PlayerResources pr = player.GetComponent<PlayerResources> ();

		if (pr.interactWithCurrentTile == false) {
			return;
		}
		//figure out where they are
		Vector3 playerLocation = player.transform.localPosition;
		Vector3 generatorLocation = this.transform.localPosition;
		//calculate distance in x and y
		float dx = playerLocation.x - generatorLocation.x;
		float dy = playerLocation.y - generatorLocation.y;

		Debug.Log (dx.ToString () + " " + dy.ToString ());

		//figure out which grid tile the player is above
		int gridx = Mathf.FloorToInt (dx / (gridWidth * 16));
		int gridy = Mathf.FloorToInt (dy / (gridHeight * 16));

		//retrieve the value at that point
		TileObject t = tileObjects [gridx, gridy];
		//if there is nothing there, quit
		if (t == null){
			return;
		}

		//compare what it is and increase the appropriate value
		switch (t.type) {
		case (byte)TileObject.resourceType.Building:
			//no need to do anything yet
			break;
		case (byte)TileObject.resourceType.Iron:
			pr.giveIron(5);
			break;
		case (byte)TileObject.resourceType.Rocks:
			pr.giveStone(10);
			break;
		case (byte)TileObject.resourceType.Trees:
			pr.giveWood(20);
			break;
		}
		*/

	}

	protected void UpdateMesh(){
		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.triangles = newTriangles.ToArray();
		mesh.uv = newUV.ToArray ();
		mesh.Optimize();
		mesh.RecalculateNormals();

		squareCount = 0;
		newVertices.Clear ();
		newTriangles.Clear ();
		newUV.Clear ();
	}

	protected void GenSquare(int x, int y, Vector2 texture, float distance = 0){
		newVertices.Add( new Vector3(worldScale * x, worldScale * y, 0));
		newVertices.Add( new Vector3(worldScale * (x + 1), worldScale * y, 0));
		newVertices.Add( new Vector3(worldScale * (x + 1), worldScale * (y - 1), 0));
		newVertices.Add( new Vector3(worldScale * x, worldScale * (y - 1), 0));
		
		//unity uses left handed triangles
		//the opposite of openGL's right handed
		//so right hand rule for poly's doesn't work here
		newTriangles.Add(squareCount*4);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+3);
		newTriangles.Add((squareCount*4)+1);
		newTriangles.Add((squareCount*4)+2);
		newTriangles.Add((squareCount*4)+3);
		
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y + tUnit));
		newUV.Add(new Vector2 (tUnit * texture.x + tUnit, tUnit * texture.y));
		newUV.Add(new Vector2 (tUnit * texture.x, tUnit * texture.y));

		squareCount++;
	}

	protected virtual void GenTerrain(){
		blocks = new byte[gridWidth, gridHeight];
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				//these are stupid random values.  More intelligent values come from derived classes
				float tmp = Mathf.Round(Random.Range (0, 6));
				tmp = tmp % 6;
				blocks[px, py] = (byte)tmp;
			}
		}
	}

	protected void BuildMesh(){
		for(int px = 0; px < blocks.GetLength (0); px++){
			for(int py = 0; py < blocks.GetLength (1); py++){
				switch(blocks[px, py]){
				case (byte)TileCodes.DryGrass:
					GenSquare(px, py, DryGrass);
					break;
				case (byte)TileCodes.Grass:
					GenSquare(px, py, Grass);
					break;
				case (byte)TileCodes.Stone:
					GenSquare(px, py, Stone);
					break;
				case (byte)TileCodes.Water:
					GenSquare(px, py, Water);
					break;
				case (byte)TileCodes.Sand:
					GenSquare (px, py, Sand);
					break;
				case (byte)TileCodes.Dirt:
					GenSquare (px, py, Dirt);
					break;
				}
			}
		}
	}

	protected virtual void AddResources(){
		for(int i = 0; i < blocks.GetLength (0); i++){
			for(int j = 0; j < blocks.GetLength (1); j++){
				//add trees
				if(blocks[i,j] == (byte)TileCodes.DryGrass ||  blocks[i,j] == (byte)TileCodes.Grass){
					Vector3 location = new Vector3(worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                               0.49f);
					GameObject newTree = Instantiate (tree, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}
				//add stone
				if(blocks[i,j] == (byte)TileCodes.Stone){
					Vector3 location = new Vector3(worldScale * i + this.transform.localPosition.x + worldScale * 0.5f,
					                               worldScale * j + this.transform.localPosition.y - worldScale * 0.5f,
					                               0.49f);
					GameObject newStone = Instantiate (stone, location, new Quaternion(0, 0, 0, 0)) as GameObject;
				}
			}
		}
	}
}
