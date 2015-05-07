using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour {

	public float lifetime = 4f;
	public bool destroyonground = false;

	private float timer;
	
	// Update is called once per frame
	void Update () {
		if(!destroyonground){
			timer += Time.deltaTime;
			if(timer > lifetime){
				Destroy(gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Ground"){

		}
	}
}
