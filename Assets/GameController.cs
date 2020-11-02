using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Ship ship;

    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<Ship>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Pause();
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
        if (Input.GetKeyDown(KeyCode.Q))
            Regenerate();
    }

    void Pause()
    {
        ship.Pause();
    }

    void Regenerate()
    {
        BoxGenerator bg = GameObject.Find("CubeContainer").GetComponent<BoxGenerator>();
        bg.Rebuild();
        Restart();
    }

    void Restart()
    {
        ship.MoveToStart();
        ship.SetDefaultSpeed();
    }
}
