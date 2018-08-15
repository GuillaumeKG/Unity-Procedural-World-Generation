using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;

	public void DrawNoiseMap(float[,] noiseMap){
		int width = noiseMap.GetLength(0);
		int height = noiseMap.GetLength(1);

		Texture2D texture = TextureGenerator.TextureFromHeightMap(noiseMap, width, height);

		Draw2DMap(texture, width, height);
	}

     
	public void DrawColorMap(Color[] colorMap, int width, int height){

		Texture2D texture = TextureGenerator.TextureFromColorMap(colorMap, width, height);
		Draw2DMap(texture, width, height);
	
	}


	public void Draw2DMap(Texture2D texture, int width, int height){

		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3(width, 1, height);
	}

}
