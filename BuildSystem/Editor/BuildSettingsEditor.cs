using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor;

 public class MultipleBuildsEditor : EditorWindow {

	public static MultipleBuildsEditor instance;
	public List<Builder> builders = new List<Builder>();

	private Vector2 scrollPos = Vector2.zero;
	
	[MenuItem ("Window/MultipleBuildsEditor")]
	public static void ShowSettings(){
		instance = (MultipleBuildsEditor)EditorWindow.GetWindow(typeof(MultipleBuildsEditor));
	}

	private void OnEnable() {
 		
 	}

	private void OnGUI() {
 		Draw();
 	}

	private void Draw(){
		GUILayout.Space(10);
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos,false,false);
		SerializedObject so = new SerializedObject(this);
		SerializedProperty sp = so.FindProperty("builders");
		EditorGUILayout.PropertyField(sp,true);
		so.ApplyModifiedProperties();
		if(GUILayout.Button("Build")){
				EditorApplication.delayCall += BuildAll;
		}
		EditorGUILayout.EndScrollView();
	}

	private void BuildAll(){
		string buildFolderPath = EditorUtility.SaveFolderPanel("Select Build Folder","","");
		if(string.IsNullOrEmpty(buildFolderPath)) return;
		foreach (var item in builders) {
			item.Build(buildFolderPath);
		}
		OpenFolder(buildFolderPath);
	}

	private void OpenFolder(string buildFolderPath){
		if(builders.Count >0 && builders[0] != null)
	 		EditorUtility.RevealInFinder(buildFolderPath+"/" + builders[0].buildTarget+"/");
	}
}
