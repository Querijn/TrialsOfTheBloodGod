using UnityEngine;
using System.Collections;

public class MoveBlock : BaseBlock{

	private Transform _transform;
	private Vector3 basepos;

	[Header("Move block in a direction")]
	public Vector3 positionmod;

	public void Awake(){
		_transform = GetComponent<Transform>() as Transform;
		basepos = _transform.position;
	}

	public override void Start()
	{
	}

	public override void Activate()
	{
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", basepos + positionmod,
			"time", 1f,
			"easing", iTween.EaseType.easeInOutQuad)
		);
	}

	public override void Deactivate()
	{


		iTween.MoveTo(gameObject, iTween.Hash(
			"position", basepos,
			"time", 1f,
			"easing", iTween.EaseType.easeInOutQuad)
		);
	}
}
