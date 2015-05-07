using UnityEngine;
using System.Collections;

public class StairsHack : MonoBehaviour {

	public float force = 4.5f;

	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == "Player"){
			GameObject t_Parent = collision.collider.transform.parent.gameObject;

			if (t_Parent.transform.position.y<(transform.position.y+0.5))
				t_Parent.rigidbody.velocity += transform.right*force;		
		}
	}
}
