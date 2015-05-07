using UnityEngine;
using System.Collections;

public class WindBlock : BaseBlock{

	private Rigidbody player;
	private Transform booster;
	private Vector3 dir;
	public float force = 50f;
	private bool isinside = false;

	public override void Start(){
		player = GameObject.Find("Character").GetComponent<Rigidbody>();
		booster = transform.Find("Booster");
		dir = booster.forward;
		booster.gameObject.SetActive(false);
	}

	public override void Activate(){
		isenabled = true;
	}

	public override void Deactivate(){
		isenabled = false;
	}

	public void OnTriggerStay(Collider col){
		isinside = true;
		if(isenabled && col.gameObject.name == "Character" && isinside){
			player.AddForce(dir * force, ForceMode.Acceleration);
		}
	}

	public void OnTriggerExit(Collider col){
		isinside = false;
	}
}