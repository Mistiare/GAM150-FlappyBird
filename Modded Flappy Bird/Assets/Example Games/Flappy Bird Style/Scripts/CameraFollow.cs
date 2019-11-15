using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;		//target for the camera to follow
	public float xOffset;			//how much x-axis space should be between the camera and target


	void Update()
	{
        if (GameObject.Find("Player")) //Ensuring that only the player is affected by the camera
        {
            transform.position = new Vector3(target.position.x + xOffset, transform.position.y, transform.position.z);
        }
		//follow the target on the x-axis only
	}
}
