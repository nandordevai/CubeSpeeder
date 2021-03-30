using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpeeder : MonoBehaviour
{
    Ship ship;
    LevelBuilder builder;
    Vector3 cameraStartPosition = new Vector3(0, 1.8f, -11f);
    GameObject startText;
    float horizontalMove = 0f;
    float smooth = 5.0f;

    void Start()
    {
        ship = GameObject.Find("Ship").GetComponent<Ship>();
        builder = GameObject.Find("CubeContainer").GetComponent<LevelBuilder>();
        startText = GameObject.Find("StartText");
        ship.onCollision.AddListener(Restart);
    }

    public void OnMove(InputValue input)
    {
        horizontalMove = input.Get<Vector2>().x;
    }

    void FixedUpdate()
    {
        Camera.main.transform.position += ship.moveSpeed * new Vector3(horizontalMove, 0, 1);
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

    public void OnStart(InputValue input)
    {
        Pause();
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
        Camera.main.transform.position = cameraStartPosition;
    }
}
