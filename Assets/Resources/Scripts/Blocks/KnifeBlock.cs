using UnityEngine;
using System.Collections;

public class KnifeBlock : BaseBlock{

	private Transform player;
	private GameObject knife;

	public float DropDelay = 2f;
	private bool hasdropped = false;
	private float timer = 0f;

	public override void Start(){
		player = GameObject.Find("Character").GetComponent<Transform>();
		knife = Resources.Load("Prefabs/Knife") as GameObject;
	}

	public override void Activate(){
		if(!hasdropped){
			Instantiate(knife, new Vector3(player.transform.position.x, player.transform.position.y + 4f, player.transform.position.z), Quaternion.identity);
			hasdropped = true;
		}
	}

	public void Update(){
		if(hasdropped){
			timer += Time.deltaTime;
			if(timer >= DropDelay){
				hasdropped = false;
				timer = 0f;
			}
		}
	}
}