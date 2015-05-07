using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BaseBlock), true)]
public class BaseBlockInspector : Editor{

	public BaseBlock tar;

	public void OnEnable(){
		tar = (BaseBlock)target;
		IconManager.SetIcon(tar.gameObject, IconManager.LabelIcon.Blue);
	}

    public override void OnInspectorGUI(){

    	if(GUILayout.Button("\nPlay Action\n")){
    		tar.Activate();
    	}

        DrawDefaultInspector();
    }
}