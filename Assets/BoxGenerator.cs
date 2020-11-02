using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGenerator : MonoBehaviour
{
    void Start()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(3, 1, 7);
        plane.transform.position = new Vector3(0, 0, 25);
        plane.GetComponent<Renderer>().material.color = Color.HSVToRGB(.6f, 0.9f, 1f);
        float z = 0;
        while (z < 50) {
            float x = -12;
            while (x < 10)
            {
                x += (Random.value + 1) * 3;
                CreateBoxAt(x, 0, z);
            }
            z += 2;
        }
    }

    private float RandomHeight()
    {
        float v1 = Random.value * 4;
        float v2 = Random.value * 4;
        if (v1 < v2)
            return v1;
        else
            return v2;
    }

    private void CreateBoxAt(float x, float y, float z)
    {
        float height = RandomHeight();
        GameObject BoxObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        BoxObj.transform.localScale = new Vector3(1, height, 1);
        BoxObj.transform.position = new Vector3(x, y + height / 2, z);
        BoxObj.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    void Update()
    {

    }
}
