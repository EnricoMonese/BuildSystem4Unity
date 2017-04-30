using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuild : MonoBehaviour {

    void OnGUI() {
		#if DEMO
        	GUILayout.Label("This is a DEMO version");
		#endif
		#if FULL
        	GUILayout.Label("This is a FULL version");
		#endif
    }
 
}
