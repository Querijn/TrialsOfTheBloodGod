using UnityEngine;
using System.Collections;

public class CrumbleBlock : BaseBlock{

	private Renderer _renderer;
	private Collider _collider;

	public override void Start(){
		isenabled = true;
		_renderer = GetComponent<Renderer>();
		_collider = GetComponent<Collider>();
	}

	public override void Activate(){
		isenabled = !isenabled;
		_renderer.enabled = isenabled;
		_collider.enabled = isenabled;
	}
}
