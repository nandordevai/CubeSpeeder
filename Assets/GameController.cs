using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Restart();
        }
    }

    void Restart()
    {
        Ship ship = GameObject.Find("Ship").GetComponent<Ship>();
        ship.MoveToStart();
        ship.SetDefaultSpeed();
    }
}
