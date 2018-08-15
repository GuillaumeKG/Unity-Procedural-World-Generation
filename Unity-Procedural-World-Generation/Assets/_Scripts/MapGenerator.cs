using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	[HideInInspector]
	public enum DrawMode {
		NoiseMap,
		ColorMap
	}

	[Header("Dimensions")]
	public int mapWidth;
	public int mapHeight;
	
	[Header("Noise Parameters")]
	public float noiseScale;
	[Range(1, 10)]
	public int octaves;
	[Range(0,1)]
	public float persistance;
	[Range(1, 10)]
	public float lacunarity;
	
	[Header("Map Id")]
	public int seed;
	public Vector2 offset;


	[Header("Textures")]
	public DrawMode drawMode;
	public TerrainType[] regions;

	[Header("Options")]

	public bool autoUpdate;




	public void GenerateMap(){
		// Generate the noise map
		float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, seed, offset);
	

		// Draw the terrain
		MapDisplay display = FindObjectOfType<MapDisplay>();

		if(drawMode == DrawMode.ColorMap){
			Color[] colorMap = CreateColorMap(noiseMap);	
			display.DrawColorMap(colorMap, mapWidth, mapHeight);
		}else if(drawMode == DrawMode.NoiseMap){
			display.DrawNoiseMap(noiseMap);
		}

	}

	private Color[] CreateColorMap(float[,] noiseMap){

		Color[] colorMap = new Color[mapWidth * mapHeight];

		// Allows optimizing the 3rd embedded for and add a break;
		//Array.Sort(regions, delegate(TerrainType x, TerrainType y) { return x.height.CompareTo(y.height); });

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float currentHeight = noiseMap[x, y];
				for (int i = 0; i < regions.Length; i++)
				{
					if(currentHeight < regions[i].height){
						colorMap[y * mapWidth + x] = regions[i].color;
						break;
					}
				}

			}
		}

		return colorMap;
	}


	// Called each time a value changes in Editor
	void OnValidate(){

	}

}

[Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color color;
}
