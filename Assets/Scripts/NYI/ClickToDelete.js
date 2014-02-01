#pragma strict

function Start () {

}

function Update () {
 if ( Input.GetMouseButtonDown(0)) //left click
    {

        var hit : RaycastHit; 
        var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition); //ray from main camera to mouse location
        //this requires a collider on the object
 
        if (Physics.Raycast (ray, hit, 100.0)) //ray length is 100 or less
        {      	
        	//Debug.Log("desu");
        	//checks the tag on the object and if the tag is equal to 'tagged', it deletes the object
        	//tags are at the very top of object properties in the inspector.
        	if(hit.collider.tag == "tagged"){
            	Destroy(hit.collider.gameObject);
            }
        }
    }
}