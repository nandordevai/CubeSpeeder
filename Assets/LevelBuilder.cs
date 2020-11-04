using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject pillarPrefab;

    GameObject container;
    int scale = 500;

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
        AddPillars();
    }

    public void Rebuild()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        AddSides();
        AddPillars();
    }

    void AddSides()
    {
        float z = -20;
        float y = 0;
        while (z < 100)
        {
            y = Random.value / 2f - 0.5f;
            AddPillarAt(-20, y, z, 6);
            y = Random.value / 2f - 0.5f;
            AddPillarAt(20, y, z, 6);
            z += .8f;
        }
        float x = -20;
        while (x < 20)
        {
            y = Random.value / 2f - 0.5f;
            AddPillarAt(x, y, 100, 6);
            x += .8f;
        }
    }

    void AddPillars()
    {
        float z = 0;
        while (z < 50)
        {
            float x = -20;
            while (x < 14)
            {
                x += (Random.value + 1.5f) * 2f;
                int height = RandomHeight();
                float y = Random.value / 2f - 0.5f;
                AddPillarAt(x, y, z, height);
            }
            z += 2;
        }
    }

    void AddPillarAt(float x, float y, float z, int height) {
        Mesh mesh = pillarPrefab.GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
        float unitHeight = mesh.bounds.size.y * (float)scale;
        for (int i = 0; i < height; i++)
        {
            float xOffset = Random.value / 8;
            float zOffset = Random.value / 8;
            GameObject pillar = Instantiate(
                pillarPrefab,
                new Vector3(x + xOffset, y + i * unitHeight, z + zOffset),
                Quaternion.identity
            );
            pillar.transform.localScale = new Vector3(scale, scale, scale);
            int theta = (int)Mathf.Floor(Random.value * 3) * 90;
            pillar.transform.Rotate(Vector3.up * theta);
            pillar.transform.SetParent(container.transform);
        }
    }

    private int RandomHeight(int min = 0)
    {
        int v1 = (int)Mathf.Floor(Random.value * 4);
        int v2 = (int)Mathf.Floor(Random.value * 4);
        if (v1 < v2)
            return v1 + min;
        else
            return v2 + min;
    }

    void Update()
    {

    }
}
