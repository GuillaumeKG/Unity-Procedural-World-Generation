using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	public void DrawNoiseMap(float[,] heightMap){
		int width = heightMap.GetLength(0);
		int height = heightMap.GetLength(1);

		Texture2D texture = TextureGenerator.TextureFromHeightMap(heightMap, width, height);

		Draw2DMap(texture, width, height);
	}

     
	public void DrawColorMap(Color[] colorMap, int width, int height){

		Texture2D texture = TextureGenerator.TextureFromColorMap(colorMap, width, height);
		Draw2DMap(texture, width, height);
	
	}

	public void DrawMesh(float[,] heightMap, Color[] colorMap, int width, int height){
		Debug.Log("-----------------------------");
		MeshData meshData = MeshGenerator.GenerateTerrainMesh(heightMap);
		Texture2D texture = TextureGenerator.TextureFromColorMap(colorMap, width, height);

		meshFilter.sharedMesh = meshData.CreateMesh();
		meshRenderer.sharedMaterial.mainTexture = texture;

	}





	public void Draw2DMap(Texture2D texture, int width, int height){

		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3(width, 1, height);
	}

}
