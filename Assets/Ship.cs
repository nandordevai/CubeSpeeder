using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class Ship : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;
    Vector3 startPosition;
    private float initialSpeed = 0.25f;

    void Start()
    {
        mesh = new Mesh();
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
        Bounds bounds = mf.sharedMesh.bounds;
        BoxCollider collider = mf.gameObject.AddComponent<BoxCollider>();
        collider.center = bounds.center;
        collider.size = bounds.size;
        CreateShip();
        UpdateMesh();
    }

    void CreateShip()
    {
        float height = .15f;
        vertices = new Vector3[]
        {
            // base
            new Vector3(0, 0, 1),
            new Vector3(height, 0, 0),
            new Vector3(-height, 0, 0),
            // back
            new Vector3(0, height, height),
            new Vector3(height, 0, 0),
            new Vector3(-height, 0, 0),
            // right side
            new Vector3(0, height, height),
            new Vector3(0, 0, 1),
            new Vector3(height, 0, 0),
            // left side
            new Vector3(0, 0, 1),
            new Vector3(0, height, height),
            new Vector3(-height, 0, 0),
        };
        triangles = new int[]
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11
        };
        startPosition = new Vector3(0, 0.5f, -9);
        transform.position = startPosition;
        transform.localScale = new Vector3(.55f, .3f, .3f);
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void Update()
    {
        transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1);
        if (transform.position.z >= 50) {
            moveSpeed = 0f;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        MoveToStart();
    }

    public void MoveToStart()
    {
        transform.position = startPosition;
    }

    public void SetDefaultSpeed()
    {
        moveSpeed = initialSpeed;
    }
}
