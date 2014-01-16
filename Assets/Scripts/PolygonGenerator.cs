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

	//list of vertices to the mesh
	public List<Vector3> newVertices = new List<Vector3>();
	//triangles how to build each section of the mesh
	public List<int> newTriangles = new List<int>();
	//UV list to tell Unity how texture is aligned to polygon
	public List<Vector2> newUV = new List<Vector2>();
	//save final terrain as this mesh
	private Mesh mesh;

	//variables to pick apart the sprite sheet
	private float tUnit = 0.25f; //percentage of the width of the image of one tile
	private Vector2 tStone = new Vector2(1, 0);
	private Vector2 tGrass = new Vector2(0, 1);

	private int squareCount = 0;

	public byte[,] blocks; //0 = air, 1 = rock, 2 = grass.  replace with enum later


	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter>().mesh;

		GenTerrain ();
		BuildMesh ();
		UpdateMesh ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateMesh(){
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

	void GenSquare(int x, int y, Vector2 texture){
		newVertices.Add( new Vector3(x, y, 0));
		newVertices.Add( new Vector3(x + 1, y, 0));
		newVertices.Add( new Vector3(x + 1, y - 1, 0));
		newVertices.Add( new Vector3(x, y - 1, 0));
		
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

	void GenTerrain(){
		blocks = new byte[10, 10];
		for (int px = 0; px < blocks.GetLength (0); px++) {
			for (int py = 0; py < blocks.GetLength (1); py++) {
				if (py == 5) {
					blocks [px, py] = 2;
				} else if (py < 5) {
					blocks [px, py] = 1;
				}else{
					blocks[px, py] = 2;
				}
			}
		}
	}

	void BuildMesh(){
		for(int px = 0; px < blocks.GetLength (0); px++){
			for(int py = 0; py < blocks.GetLength (1); py++){
				if(blocks[px, py] == 1){
					GenSquare(px, py, tStone);
				}else if(blocks[px, py] == 2){
					GenSquare(px, py, tGrass);
				}
			}
		}
	}
}
