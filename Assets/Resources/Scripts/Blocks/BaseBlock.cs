using UnityEngine;
using System.Collections;

public enum ControlType { SinglePress, Toggle, Hold, Cycle };

public class BaseBlock : MonoBehaviour {

	public KeyCode key;
	public ControlType control = ControlType.SinglePress;
	
	//[HideInInspector]
	public bool isenabled = false;

	public virtual void Start(){

	}

	public virtual void Activate(){

	}

	public virtual void Deactivate(){
		
	}

	public virtual void DestroyMe(){

	}


}
