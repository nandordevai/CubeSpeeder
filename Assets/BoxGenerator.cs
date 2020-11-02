using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    GameObject container;

    void Start()
    {
        container = GameObject.Find("CubeContainer");
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(4, 1, 12);
        plane.transform.position = new Vector3(0, 0, 40);
        MeshRenderer mr = plane.GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
        mr.material.color = Color.HSVToRGB(.6f, 0.9f, 1f);
        AddSides();
        AddCubes();
    }

    public void Rebuild()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        AddCubes();
    }

    void AddSides()
    {
        float z = -20;
        while (z < 100)
        {
            CreateSideBoxAt(-20, 0, z);
            CreateSideBoxAt(20, 0, z);
            z += 1;
        }
        float x = -20;
        while (x < 20)
        {
            CreateSideBoxAt(x, 0, 100);
            x += 1;
        }
    }

    void AddCubes()
    {
        float z = 0;
        while (z < 50)
        {
            float x = -20;
            while (x < 14)
            {
                x += (Random.value + 1.5f) * 2;
                CreateBoxAt(x, 0, z);
            }
            z += 2;
        }
    }

    private float RandomHeight(int min = 0)
    {
        float v1 = Random.value * 4;
        float v2 = Random.value * 4;
        if (v1 < v2)
            return v1 + min;
        else
            return v2 + min;
    }

    private void CreateSideBoxAt(float x, float y, float z)
    {
        float height = RandomHeight(4);
        _CreateBoxAt(x, y, z, height);
    }

    private void CreateBoxAt(float x, float y, float z)
    {
        float height = RandomHeight();
        GameObject box = _CreateBoxAt(x, y, z, height);
        box.transform.SetParent(container.transform);
    }

    private GameObject _CreateBoxAt(float x, float y, float z, float height)
    {
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.transform.localScale = new Vector3(1, height, 1);
        box.transform.position = new Vector3(x, y + height / 2, z);
        box.GetComponent<Renderer>().material.color = Color.HSVToRGB(0f, 0f, .9f);
        MeshRenderer mr = box.GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));
        box.AddComponent<BoxCollider>();
        return box;
    }

    void Update()
    {

    }
}
