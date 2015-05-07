using UnityEngine;
using System.Collections;

public class RotateCoin : MonoBehaviour {

	public float speed = 45f;
	private float ymod = 0f;
	private Vector3 startpos;
	private float timer = 0f;
	private bool ispickedup = false;

	void Start () {
		startpos = transform.position;
	}
	
	void Update () {
		transform.Rotate(Vector3.up * speed * Time.deltaTime);
		if(!ispickedup){
			timer += Time.deltaTime;
			ymod = Mathf.PingPong(timer, 1);
			transform.position = Vector3.Lerp(transform.position, new Vector3(startpos.x, startpos.y + (ymod/2f), startpos.z), Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "Player" && !ispickedup){
			Game.instance.GrabCoin(transform);
			ispickedup = true;
			Destroy(gameObject);
		}
	}
}
