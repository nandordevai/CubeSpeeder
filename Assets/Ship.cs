using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof (MeshFilter))]
public class Ship : MonoBehaviour
{
    public float moveSpeed = 0;
    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;
    Vector3 startPosition;
    private float initialSpeed = 0.25f;
    MeshFilter mf;
    bool finished = false;

    void Start()
    {
        mesh = new Mesh();
        mf = GetComponent<MeshFilter>();
        mf.mesh = mesh;
        startPosition = new Vector3(0, 0.5f, -9);
        CreateShip();
    }

    void CreateShip()
    {
        float height = .15f;
        vertices =
            new Vector3[]
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
                new Vector3(-height, 0, 0)
            };
        triangles = Enumerable.Range(0, vertices.Length).ToArray();
        transform.localScale = new Vector3(.55f, .3f, .3f);
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        BoxCollider collider = mf.gameObject.AddComponent<BoxCollider>();
        Bounds bounds = mesh.bounds;
        collider.center = bounds.center;
        collider.size = bounds.size;
    }

    void Update()
    {
    }

    void OnCollisionEnter(Collision c)
    {
        StopMoving();
        MoveToStart();
    }

    public void Pause()
    {
        if (finished)
            return;

        if (Stopped())
            StartMoving();
        else
            StopMoving();
    }

    public bool Stopped()
    {
        return moveSpeed == 0;
    }

    public void MoveToStart()
    {
        transform.position = startPosition;
        finished = false;
    }

    public void StartMoving()
    {
        moveSpeed = initialSpeed;
    }

    public void StopMoving()
    {
        moveSpeed = 0;
    }

    public void Finish()
    {
        finished = true;
        moveSpeed *= 0.95f;
        if (moveSpeed < .01f)
        {
            moveSpeed = 0;
        }
    }
}
