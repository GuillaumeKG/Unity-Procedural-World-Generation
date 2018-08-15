using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI(){
		MapGenerator mapGen = (MapGenerator)target;

		// Draw default layout of inpsector panel
		// DrawDefaultInspector() return true if at least a value has changed
		if(DrawDefaultInspector()){
			if(mapGen.autoUpdate){
				mapGen.GenerateMap();
			}
		}

		// Create a button labeled "Generate" and attach a behavior on OnClick event
		if(GUILayout.Button("Generate")){
			mapGen.GenerateMap();
		}	
	}
}
