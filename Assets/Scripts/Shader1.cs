using UnityEngine;
using System.Collections;

public class Shader1 : MonoBehaviour
{
    public Shader shader;

    // Use Awake() instead of Start() to run code even if the script is not initially enabled
    void Awake()
    {
        if (this.gameObject.GetComponent<MeshFilter>() == null)
        {
            // Add a MeshFilter component to this entity. This essentially comprises of a
            // mesh definition, which in this example is a collection of vertices, colours 
            // and triangles (groups of three vertices). 
            MeshFilter cubeMesh = this.gameObject.AddComponent<MeshFilter>();
            cubeMesh.mesh = this.CreateCubeMesh();
        }

        if (this.gameObject.GetComponent<MeshRenderer>() == null)
        {
            // Add a MeshRenderer component. This component actually renders the mesh that
            // is defined by the MeshFilter component.
            this.gameObject.AddComponent<MeshRenderer>();
        }
    }

    // Using OnEnable to let students test the different shaders on the same object
    // at runtime (multiple script instances).
    void OnEnable()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();
        renderer.material.shader = this.shader;
    }

    void Update()
    {
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Set blend uniform parameter in an oscillating fashion to demonstrate 
        // blending shader (challenge question)
        renderer.material.SetFloat("_BlendFct", (Mathf.Sin(Time.time) + 1.0f) / 2.0f);
    }

    // Method to create a cube mesh with coloured vertices
    Mesh CreateCubeMesh()
    {
        Mesh m = new Mesh();
        m.name = "Cube";

        // Define the vertices. These are the "points" in 3D space that allow us to
        // construct 3D geometry (by connecting groups of 3 points into triangles).
        m.vertices = new[] {
            new Vector3(-1.0f, 1.0f, -1.0f), // Top
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),

            new Vector3(-1.0f, -1.0f, -1.0f), // Bottom
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            new Vector3(-1.0f, -1.0f, -1.0f), // Left
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),

            new Vector3(1.0f, -1.0f, -1.0f), // Right
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),

            new Vector3(-1.0f, 1.0f, 1.0f), // Front
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            new Vector3(-1.0f, 1.0f, -1.0f), // Back
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f)
        };

        

        // Define the uv coordinates
        m.uv = new[] {
            new Vector2(0.0f, 0.666f), // Top
            new Vector2(0.0f, 1.0f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.0f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.333f, 0.666f),

            new Vector2(0.333f, 0.333f), // Bottom
            new Vector2(0.666f, 0.0f),
            new Vector2(0.333f, 0.0f),
            new Vector2(0.333f, 0.333f),
            new Vector2(0.666f, 0.333f),
            new Vector2(0.666f, 0.0f),

            new Vector2(0.666f, 0.666f), // Left
            new Vector2(0.333f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.666f, 0.666f),
            new Vector2(0.333f, 1.0f),
            new Vector2(0.666f, 1.0f),

            new Vector2(0.0f, 0.333f), // Right
            new Vector2(0.333f, 0.666f),
            new Vector2(0.333f, 0.333f),
            new Vector2(0.0f, 0.333f),
            new Vector2(0.0f, 0.666f),
            new Vector2(0.333f, 0.666f),

            new Vector2(0.666f, 0.666f), // Front
            new Vector2(0.333f, 0.333f),
            new Vector2(0.333f, 0.666f),
            new Vector2(0.666f, 0.666f),
            new Vector2(0.666f, 0.333f),
            new Vector2(0.333f, 0.333f),

            new Vector2(0.0f, 0.333f), // Back
            new Vector2(0.333f, 0.333f),
            new Vector2(0.333f, 0.0f),
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 0.333f),
            new Vector2(0.333f, 0.0f)
        };

        // Automatically define the triangles based on the number of vertices
        int[] triangles = new int[m.vertices.Length];
        for (int i = 0; i < m.vertices.Length; i++)
            triangles[i] = i;

        m.triangles = triangles;

        return m;
    }
}
