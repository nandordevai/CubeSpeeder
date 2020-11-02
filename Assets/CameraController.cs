using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.25f;

    void Update () {
        transform.position += moveSpeed * new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1);
        if (transform.position.z >= 50) {
            moveSpeed = 0;
        }
    }
}
