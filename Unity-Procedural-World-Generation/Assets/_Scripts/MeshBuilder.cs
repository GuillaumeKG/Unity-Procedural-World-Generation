using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder {

	// Contains the list of veterx position
	private List<Vector3> vertices = new List<Vector3>();
	// Contain the index reference in vertices to create triangles
	// triangles[0]=0 means the first vertex of the first triangle is the vertex vertices[0]
	private List<int> triangles = new List<int>();
	
	// List of normals 
	private List<Vector3> normals = new List<Vector3>();
	// 
	private List<Vector2> uvs = new List<Vector2>();

	private List<int>[] submeshIndices;


	public MeshBuilder(int submeshCount){
		submeshIndices= new List<int>[submeshCount]; 

		for (int i = 0; i < submeshCount; i++)
		{
			submeshIndices[i] = new List<int>();
		}

	}


	public void BuildTriangle(Vector3 vert0, Vector3 vert1, Vector3 vert2, int submesh){
		Vector3 normal = Vector3.Cross(vert1 - vert0, vert2 - vert0).normalized;
	}

	public void BuildTriangle(Vector3 vert0, Vector3 vert1, Vector3 vert2, Vector3 normal, int submesh){

		int vert0Idx = vertices.Count;
		int vert1Idx = vertices.Count + 1;
		int vert2Idx = vertices.Count + 2;

		vertices.Add(vert0);
		vertices.Add(vert1);
		vertices.Add(vert2);

		triangles.Add(vert0Idx);
		triangles.Add(vert1Idx);
		triangles.Add(vert2Idx);

		submeshIndices[submesh].Add(vert0Idx);
		submeshIndices[submesh].Add(vert2Idx);
		submeshIndices[submesh].Add(vert2Idx);

		normals.Add(normal);
		normals.Add(normal);
		normals.Add(normal);

		uvs.Add(new Vector2(0, 0));
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(1, 1));
		
	}

	public Mesh CreateMesh(){
		Mesh mesh = new Mesh();

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		
		mesh.normals = normals.ToArray();
		mesh.uv = uvs.ToArray();

		mesh.subMeshCount = submeshIndices.Length;
		

		for(int i = 0; i < submeshIndices.Length; i++){
			if(submeshIndices[i].Count < 3){
				mesh.SetTriangles(new int[3], i);
			}else{
				mesh.SetTriangles(submeshIndices[i].ToArray(), i);
			}
		}

		return mesh;

	}
}