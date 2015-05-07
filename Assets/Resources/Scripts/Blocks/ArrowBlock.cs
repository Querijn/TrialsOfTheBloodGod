using UnityEngine;
using System.Collections;

public class ArrowBlock : BaseBlock{

	private Transform booster;
	private Quaternion rot;

	public GameObject arrow_obj;

	public float ShootDelay = 1f;
	private bool hasshot = false;
	private float timer = 0f;

	public override void Start(){
		booster = transform.Find("Booster");
		rot = booster.rotation;
		booster.gameObject.SetActive(false);
	}

	public override void Activate(){
		if(!hasshot){
			GameObject arrow = Instantiate(arrow_obj, transform.position, Quaternion.identity) as GameObject;
			rot.eulerAngles = new Vector3(90, 0, rot.eulerAngles.z);
			arrow.transform.rotation = rot;
		}
	}

	public void Update(){
		if(hasshot){
			timer += Time.deltaTime;
			if(timer >= ShootDelay){
				hasshot = false;
				timer = 0f;
			}
		}
	}
}