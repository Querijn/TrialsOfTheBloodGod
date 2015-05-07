using UnityEngine;
using System.Collections;

public class SpikeBlock : BaseBlock{

	public Transform spike;

	public float TimeToMoveUp = 0.05f;
	public float TimeToGoDown = 2f;

	private float base_y;

	public override void Start(){
		if(spike == null){
			Debug.LogError("<color=red>SpikeBlock</red> doesn't have the spike variable set.");
		}

		base_y = spike.localPosition.y;
	}

	public override void Activate(){
		base.Activate();
		iTween.MoveTo(spike.gameObject, iTween.Hash(
			"y", 0.244,
			"time", TimeToMoveUp,
			"islocal", true)
		);
	}

	public override void Deactivate(){
		iTween.MoveTo(spike.gameObject, iTween.Hash(
			"y", base_y,
			"time", TimeToGoDown,
			"islocal", true,
			"easing", iTween.EaseType.linear)
		);
	}
}
