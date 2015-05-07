using UnityEngine;
using System.Collections;

public class SpawnBlock : BaseBlock{

	public GameObject spawn;

	public override void Start(){
		GetComponent<Renderer>().enabled = false;
	}

	public override void Activate(){
		Instantiate(spawn, transform.position, Quaternion.identity);
	}

	public override void Deactivate(){
		
	}
}
