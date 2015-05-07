using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerBlock : MonoBehaviour{

	public BaseBlock[] blocks;
	private KeyCode[] keys = new[] {
		KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
		KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R
	};

	public bool GodMode = false;

	void Start(){
		Shuffle(ref keys);
		blocks = GameObject.FindObjectsOfType(typeof(BaseBlock)) as BaseBlock[];
		AssignKeys();

		if(GodMode){
			ShowKeys();
		}
	}
	
	void Update(){
		foreach(BaseBlock block in blocks){
			if(Input.GetKeyDown(block.key)){
				block.Activate();
			}

			if(block.control == ControlType.Hold && Input.GetKeyUp(block.key)){
				block.Deactivate();
			}
		}
	}

	private void AssignKeys(){
		int keycount = 0;
		for(int i = 0; i < blocks.Length; i++){
			if(i > keys.Length-1){
				keycount = 0;
				Shuffle(ref keys);
			}
			blocks[i].key = keys[keycount];
			blocks[i].gameObject.name = keys[keycount].ToString() + " - " + blocks[i].gameObject.name;
			keycount++;
		}
	}

	private void Shuffle(ref KeyCode[] texts){
        for(int t = 0; t < texts.Length; t++){
            KeyCode tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    private void ShowKeys(){
    	GameObject textprefab = Resources.Load("Prefabs/Text") as GameObject;
    	foreach(BaseBlock block in blocks){
    		GameObject tmp = Instantiate(textprefab, block.gameObject.transform.position, Quaternion.identity) as GameObject;
    		Quaternion tmprot = tmp.transform.rotation;
    		tmprot.eulerAngles = new Vector3(90, -90, 0);
    		tmp.transform.rotation = tmprot;

    		TextMesh mesh = tmp.GetComponent<TextMesh>() as TextMesh;
    		string str = block.key.ToString();

    		mesh.text = str.Replace("Alpha", "");

    		PopUp popup = block.gameObject.GetComponent<PopUp>() as PopUp;
    		popup.text = mesh.gameObject.GetComponent<Renderer>();
    	}
    }
}
