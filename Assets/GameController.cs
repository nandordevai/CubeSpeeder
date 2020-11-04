using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Ship ship;
    new GameObject camera;
    LevelBuilder builder;
    Vector3 cameraStartPosition = new Vector3(0, 1.8f, -11f);
    GameObject startText;

    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<Ship>();
        camera = GameObject.Find("Camera");
        builder = GameObject.Find("CubeContainer").GetComponent<LevelBuilder>();
        startText = GameObject.Find("StartText");
        ship.onCollision.AddListener(Restart);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Pause();
        if (Input.GetKeyDown(KeyCode.R))
            Restart();
        if (Input.GetKeyDown(KeyCode.Q))
            Regenerate();

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float smooth = 5.0f;

        camera.transform.position += ship.moveSpeed * new Vector3(horizontalMove, 0, 1);
        Quaternion target = Quaternion.Euler(0, 0, -horizontalMove * 30f);
        ship.transform.rotation = Quaternion.Slerp(
            ship.transform.rotation,
            target,
            Time.deltaTime * smooth
        );
        ship.transform.position += ship.moveSpeed * new Vector3(horizontalMove, 0, 1);
        if (ship.transform.position.z >= 50)
        {
            ship.Finish();
        }
    }

    void Pause()
    {
        ship.Pause();
    }

    void Regenerate()
    {
        builder.Rebuild();
        Restart();
    }

    void Restart()
    {
        ship.StopMoving();
        ship.MoveToStart();
        camera.transform.position = cameraStartPosition;
    }
}
