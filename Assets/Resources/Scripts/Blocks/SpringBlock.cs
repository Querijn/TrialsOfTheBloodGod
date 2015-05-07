using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpringBlock : BaseBlock{

	public List<Rigidbody> players;
	private Transform booster;
	private Vector3 dir;

	public float upForce = 50f;
	public float forwardForce = 50f;

	public bool RemovePlayerVelocity = false;

	public Animator _animator;

	public override void Start(){
		booster = transform.Find("Booster");
		dir = booster.forward;
		booster.gameObject.SetActive(false);
		_animator = GetComponent<Animator>();
	}

	public override void Activate()
	{
		_animator.Play("SpringBlock_Boost");

		foreach(Rigidbody player in players)
		{
			if(isenabled && player != null)
			{
				if(RemovePlayerVelocity)
				{
					player.velocity = Vector3.zero;
				}

				player.AddForce(dir * forwardForce, ForceMode.Impulse);
				player.AddForce(Vector3.up * upForce, ForceMode.Impulse);
			}
		}
	}

	public void OnTriggerEnter(Collider col)
	{
		isenabled = true;
		if(col.gameObject.tag == "Player")
			players.Add(col.gameObject.GetComponent<Rigidbody>());
	}

	public void OnTriggerExit(Collider col)
	{
		isenabled = false;
		if(col.gameObject.tag == "Player")
			players.Remove(col.gameObject.GetComponent<Rigidbody>());
	}
}