using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	public static Game instance;

	public float time = 0f;
	public int coins;
	public int maxcoins = 0;

	private GameObject[] coinlocations;

	public GameObject exit;

	private Vector3 start;

	public bool israce = false;
	public bool fuckthishack = false; //cba to go through other files to change names
	public Text countdown;
	private int count = 0;

	void Awake(){
		instance = this;
	}

	void Start(){
		coinlocations = GameObject.FindGameObjectsWithTag("CoinLocation");
		maxcoins = GameObject.FindGameObjectsWithTag("Coin").Length;

		if(exit == null){
			Debug.LogError("The exit variable is not set in Game on object Essentials");
		}
		else{
			// transform.position = new Vector3(start.x, start.y-4.2f, start.z);
			// exit.GetComponent<Renderer>().enabled = false;
		}

		if(maxcoins > 0){
			//disable exit;
		}

		fuckthishack = israce;

		if(israce){
			countdown = GameObject.Find("Countdown").GetComponent<Text>();
			iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "Filler", "oncomplete", "NextCountdown"));
		}
		else{
			GameObject.Find("Countdown").SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}

	public void GrabCoin(Transform tomove){
		Debug.Log("picked up");
		coins++;

		if(coins >= maxcoins){
		}
	}

	public void Filler(float f){
		countdown.transform.localScale = new Vector3(f, f, f);

	}

	public void NextCountdown(){
		count++;
		countdown.transform.localScale = new Vector3(1f, 1f, 1f);
		switch(count){
			case 1:
				iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "Filler", "oncomplete", "NextCountdown"));
				countdown.text = "2";
				break;
			case 2:
				iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "Filler", "oncomplete", "NextCountdown"));	
				countdown.text = "1";
				break;
			case 3:
				countdown.text = "GO!";
				iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 0.1f, "onupdate", "FadeOut", "delay", 1f));
				israce = false;
				break;
		}
	}

	public void FadeOut(float f){
		Color col = countdown.color;
		col.a = f;
		countdown.color = col;
	}
}
