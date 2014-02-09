#pragma strict

function Start () {

}
static var speed : int = 5;
function Update () {
       if (Input.GetKey (KeyCode.W)) transform.Translate (Vector3(0,0,1) * Time.deltaTime*speed);
       if (Input.GetKey (KeyCode.S)) transform.Translate (Vector3(0,0,-1) * Time.deltaTime*speed);
       if (Input.GetKey (KeyCode.A)) transform.Translate (Vector3(-1,0,0) * Time.deltaTime*speed);
       if (Input.GetKey (KeyCode.D)) transform.Translate (Vector3(1,0,0) * Time.deltaTime*speed);
}
 
 function OnTriggerEnter () 
{
	Debug.Log("Thats the end.");
   	
}