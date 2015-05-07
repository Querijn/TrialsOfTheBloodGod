using UnityEngine;
using System.Collections;

public class FlameThrowerBlock : BaseBlock{

	public GameObject deathzone;
	public GameObject flamethrower;

	public override void Start(){
		flamethrower.SetActive(false);
		deathzone.SetActive(false);
	}

	public override void Activate(){
		isenabled = !isenabled;
		deathzone.SetActive(isenabled);
		flamethrower.SetActive(isenabled);
	}
}
