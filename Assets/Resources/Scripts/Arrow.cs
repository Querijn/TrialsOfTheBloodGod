using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public float speed = 10f;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
