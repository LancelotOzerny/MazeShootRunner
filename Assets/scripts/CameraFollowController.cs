using System.Runtime.InteropServices;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [Header("Camera Follower")]
    [SerializeField] private GameObject _followObject = null;

    [Header("Camera Follow Settings")]
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private Vector2 _movementDirection;

    private Vector2 FollowPos { get => _followObject.transform.position; }
    private Vector3 Pos { get => transform.position; }

    private void Start()
    {
        transform.position = new Vector3(
            FollowPos.x,
            FollowPos.y,
            Pos.z
        );
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(
            Pos,
            new Vector3(
                FollowPos.x,
                FollowPos.y,
                Pos.z
            ),
            _speed * Time.deltaTime
        );
    }

    private void GetMovementDirection()
    {
        // Ставим позицию X
        if ((Pos.x > FollowPos.x && _movementDirection.x > 0.5f) || (Pos.x < FollowPos.x && _movementDirection.x < -0.5f))
        {
            _movementDirection.x = 0;
                transform.position = new Vector3(
                FollowPos.x,
                Pos.y,
                Pos.z
            );
        }
        else if (Pos.x > FollowPos.x) _movementDirection.x = -1;
        else if (Pos.x < FollowPos.x) _movementDirection.x = 1;

        // Ставим позицию Y
        if ((Pos.y > FollowPos.y && _movementDirection.y > 0.5f) || (Pos.y < FollowPos.y && _movementDirection.y < -0.5f))
        {
            _movementDirection.y = 0;
            transform.position = new Vector3(
            Pos.x,
            FollowPos.y,
            Pos.z
        );
        }
        else if (Pos.y > FollowPos.y) _movementDirection.y = -1;
        else if (Pos.y < FollowPos.y) _movementDirection.y = 1;
    }
}
