using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class Builder : ScriptableObject {

	#if UNITY_EDITOR
	[Header("Normal Settings")]
	public string buildName;
	public BuildTarget buildTarget = BuildTarget.StandaloneWindows64;
	public bool developerBuild;

	[Header("Define Symbols")]
	public bool setDefineSymbols;
	public string defineSymbols;
	public BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone;

	[Header("Scenes")]
	public Object[] scenes;
	
	
	private string[] GetScenes () {
		List<string> scenesStrings = new List<string>();
		foreach (Object s in scenes) {
			scenesStrings.Add(AssetDatabase.GetAssetPath(s));
		}
		return scenesStrings.ToArray();
	}

	public void Build(string buildFolderPath) {
		string previousSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
		if(setDefineSymbols)
			PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,defineSymbols);

		string[] scenes = GetScenes();
		string buildNamePath = buildFolderPath + "/" + buildTarget + "/" + buildName;
		string buildFullPath = buildNamePath + "/" + buildName+".exe";

		DeleteBuildFolder(buildNamePath);
		BuildOptions bo = developerBuild?BuildOptions.Development:BuildOptions.None;
		BuildPipeline.BuildPlayer (scenes, buildFullPath, buildTarget,BuildOptions.None);

		PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup,previousSymbols);
	}

	private void DeleteBuildFolder(string buildFolderPath) {
		if (System.IO.Directory.Exists (buildFolderPath)) {
			System.IO.Directory.Delete (buildFolderPath, true);
		}
	}
	#endif
}
