using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[ExecuteInEditMode]
public class Experimental : MonoBehaviour{

	//run a script that modifies all icons to a different tag that works with the snap
	[MenuItem ("Environment/Test Env")]
	static void SetupLevel(){
		foreach(PopUp tmp in GameObject.FindObjectsOfType(typeof(PopUp)) as PopUp[]){
			DestroyImmediate(tmp);
		}

		AddComponents("Environment");
		//AddComponents("Background");
		AddComponents("Level");
		AddComponentsDirect("Entities");
	}

	public static void AddComponents(string parent){
		Transform[] trans = GameObject.Find(parent).GetComponentsInChildren<Transform>();
		foreach(Transform child in trans){
			if(child.gameObject.GetComponent<MeshRenderer>() != null){
				if(child.gameObject.GetComponent<PopUp>() == null && !child.parent.gameObject.name.Contains("Stairs")){
		 			child.gameObject.AddComponent<PopUp>();
		 		}
		 	}
		}
	}

	public static void AddComponentsDirect(string parent){
		foreach(Transform child in GameObject.Find(parent).transform){
			if(child.gameObject.GetComponent<MeshRenderer>() != null || child.gameObject.name.Contains("SpringBlock")){
				if(child.gameObject.GetComponent<PopUp>() == null){
		 			child.gameObject.AddComponent<PopUp>();
		 		}
		 	}
		}
	}
}