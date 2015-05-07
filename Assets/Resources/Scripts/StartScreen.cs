using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

	public Image playerPanel1;
	public Image playerPanel2;
	public Color confirmColor = new Color (1,1,1,0);

	private bool keyboardConfirm = false;
	private bool controllerConfirm = false;

	private float timer = 0f;

	void Start(){
	}
	
	void Update(){
		if(Input.anyKey && !keyboardConfirm){
			playerPanel2.color = confirmColor;
			keyboardConfirm = true;
		}

		if(Input.GetButtonDown("A_1") && !controllerConfirm){
			playerPanel1.color = confirmColor;
			controllerConfirm = true;
		}

		if(controllerConfirm){
			timer += Time.deltaTime;
		}

		if(controllerConfirm && keyboardConfirm && (Input.anyKey || Input.GetButtonDown("A_1")) && timer >= 0.1f ){
			Application.LoadLevel("ArtLevel01");		
		}
	}
}
