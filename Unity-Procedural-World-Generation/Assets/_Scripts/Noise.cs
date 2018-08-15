using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {

	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistance, float lacunarity, int seed, Vector2 offset){
		float[,] noiseMap = new float[mapWidth, mapHeight];


		// To be ablle to regenerate exactly the same noisemap, we use a seed
		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];

		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) + offset.y;

			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		// Scale
		if (scale <= 0){
			scale = 0.001f;
		}

		// Used to apply scale from center of the plan
		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;

		// Used to normalized the final noiseHeight to be in range [0, 1]
		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				// We calculate the perlin value for each octave
				// Then combine them together yo get the fine-grained PerlinValue at the en dof the loop
				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

					// perlinNoise range is [0, 1], so '*2 - 1' change the range to [-1, 1]
					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;		
				}

				if(noiseHeight >  maxNoiseHeight){
					maxNoiseHeight = noiseHeight;
				}else if(noiseHeight <  minNoiseHeight){
					minNoiseHeight = noiseHeight;
				}

				noiseMap[x, y] = noiseHeight;

			}
		}

		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				// Clamp to [0, 1] relative to min and max value
				noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
			}
			
		}


		return noiseMap;
	}
}
