using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Exit : MonoBehaviour {

	public string toload;


	void Start(){


	}


	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			if(Game.instance.fuckthishack){
				string id = col.gameObject.name.Replace("Character ", "");
				Game.instance.countdown.text = "Player " + id + " wins!";
				Color tmpcol = Game.instance.countdown.color;
				tmpcol.a = 1f;
				Game.instance.countdown.color = tmpcol;
				Game.instance.israce = true;
				iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 2f, "onupdate", "Filler", "oncomplete", "Done"));
			}
			else{
				Application.LoadLevel(toload);
			}
		}
	}

	public void Filler(float f){}

	public void Done(){
		Application.LoadLevel(toload);
	}
}
