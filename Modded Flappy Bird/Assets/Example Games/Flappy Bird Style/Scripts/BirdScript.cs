using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour 
{
	public bool isDead = false;		//has the player collided with a wall?
    public float upForce;
    public float forwardSpeed;
    Rigidbody rigidbody;
    Animator anim;
	bool flap = false;				//has the player triggered a "flap"?


	void Start()
	{
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //set the bird moving forward

        rigidbody.velocity = new Vector3(forwardSpeed, 0, 0);
	}

	void Update()
	{
		//don't allow control if the bird has died
		if (isDead)
			return;
		//look for input to trigger a "flap"
		if (Input.anyKeyDown)
			flap = true;
	}

	void FixedUpdate()
	{
		//if a "flap" is triggered...
		if (flap) 
		{
			flap = false;
            //...zero out the birds current y velocity before...
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
            //..giving the bird some upward force
            rigidbody.AddForce(new Vector3(0, upForce, 0));
            GetComponent<Animator>().speed = 1.4f;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		//if the bird collides with something set it to dead...
		isDead = true;
		//...and tell the game control about it
		GameControlScript.current.BirdDied ();
        Destroy(gameObject);
	}
}
