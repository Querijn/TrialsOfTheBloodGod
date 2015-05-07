using UnityEngine;
using System.Collections;

public class PopUp : MonoBehaviour {

	private Vector3 start;
	private GameObject[] players;
	private Renderer _renderer;

	private bool hasanimated = false;

	private bool isspecial = false;

	private float distance = 500f;

	public Renderer text;

	// Use this for initialization
	void Start () {
		start = transform.position;
		players = GameObject.FindGameObjectsWithTag("Player");
		_renderer = GetComponent<Renderer>() as Renderer;


		if(transform.parent.gameObject.name == "Background"){
			distance = 25;
		}

		if(Vector3.Distance(start, players[0].transform.position) > distance || (gameObject.name.Contains("Exit") && Game.instance.maxcoins != 0)){
			_renderer.enabled = false;
			transform.position = new Vector3(start.x, start.y-2f, start.z);
			if(text != null){
				text.enabled = false;
			}
		}

		if(transform.parent.name == "Entities"){
			isspecial = true;

			if(gameObject.name.Contains("MoveBlock") || gameObject.name.Contains("SpringBlock") || gameObject.name.Contains("FlameThrowerBlock")){
				ToggleRenderers(false);
			}
			else if(gameObject.name.Contains("SpikeBlock")){
				transform.Find("Assets_Spikes_1x1x1").GetComponent<Renderer>().enabled = false;
			}
		}
	}

	void Update(){
		foreach(GameObject _player in players){
			Transform player = _player.transform;
			if(Vector3.Distance(start, player.position) < distance && !hasanimated){
				if(!isspecial || (gameObject.name.Contains("Exit") && Game.instance.coins >= Game.instance.maxcoins)){
					_renderer.enabled = true;
				}
				else if(gameObject.name.Contains("MoveBlock") || gameObject.name.Contains("SpringBlock") || gameObject.name.Contains("FlameThrowerBlock")){
					ToggleRenderers(true);
				}
				else if(gameObject.name.Contains("SpikeBlock")){
					transform.Find("Assets_Spikes_1x1x1").GetComponent<Renderer>().enabled = true;
				}
				else{
					_renderer.enabled = true;
				}

				if(!gameObject.name.Contains("Exit") || (gameObject.name.Contains("Exit") && Game.instance.coins >= Game.instance.maxcoins) || (gameObject.name.Contains("Exit") && Game.instance.maxcoins == 0)){
					iTween.MoveTo(gameObject, iTween.Hash(
						"position", start,
						"time", Random.Range(0.2f, 0.6f),
						"easetype", iTween.EaseType.easeOutQuad,
						"oncomplete", "DestroyMe")
					);

					hasanimated = true;
				}

				if(text != null){
					text.enabled = true;
				}
			}
		}
	}

	public void ToggleRenderers(bool dir){
		Transform[] trans = gameObject.GetComponentsInChildren<Transform>();
		foreach(Transform child in trans){
			Renderer rend = child.gameObject.GetComponent<MeshRenderer>();
			if(rend != null){
				rend.enabled = dir;
			}
		}
	}

	public void DestroyMe(){
		Destroy(this);
	}
}
