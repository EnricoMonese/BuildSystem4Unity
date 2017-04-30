using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Builder)),CanEditMultipleObjects]
public class BuilderEditor : Editor {
	private Builder[] builders;
 
	private void OnEnable () {
		builders = new Builder[targets.Length];
		for (int i = 0; i <targets.Length; i++) {
			builders[i] = (Builder)targets[i];
		}
	}

	public override void OnInspectorGUI () {
		DrawDefaultInspector();
		GUILayout.Space(20);
		if(GUILayout.Button("Build")){
			EditorApplication.delayCall += Build;
		}
 	}

	private void Build(){
		string buildFolderPath = EditorUtility.SaveFolderPanel("Select Build Folder","","");
		if(string.IsNullOrEmpty(buildFolderPath)) return;
		foreach (var item in builders) {
			item.Build(buildFolderPath);
		}
		OpenFolder(buildFolderPath);
	}

	private void OpenFolder(string buildFolderPath){
		if(builders.Length>1){
			EditorUtility.RevealInFinder(buildFolderPath+"/" + builders[0].buildTarget + "/");
		}else{
			EditorUtility.RevealInFinder(buildFolderPath+"/" + builders[0].buildTarget + "/" +builders[0].buildName+"/");
		}
	}
}

