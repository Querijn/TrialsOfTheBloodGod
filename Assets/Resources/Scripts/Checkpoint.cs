using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

	public Animator _animator;
	public List<GameObject> players = new List<GameObject>();

	private int maxcount = 0;


	public void Start(){
		_animator = GameObject.Find("Canvas").GetComponent<Animator>();
		maxcount = GameObject.FindGameObjectsWithTag("Player").Length;
	}

	public void OnTriggerEnter(Collider col){
		Controller tmp = col.gameObject.GetComponent<Controller>();
		if(col != null && !IsInList(col.gameObject) && col.gameObject.tag == "Player"){
			players.Add(col.gameObject);
			tmp.SetStartPos(transform);

			_animator.Play("Checkpoint_Appear");
			
			if(players.Count == maxcount)
				Destroy(gameObject);
		}
	}

	public bool IsInList(GameObject tar){
		bool isinlist = false;
		foreach(GameObject p in players){
			if(p == tar){
				isinlist = true;
				break;
			}
		}


		return isinlist;
	}
}
