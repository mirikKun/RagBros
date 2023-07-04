using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    /// <summary>
    /// Camera offset relative to the character
    /// </summary>
    [SerializeField] private Vector2 offset;

    private Transform _transform;

    /// <summary>
    /// The camera follows the character without changing the height
    /// </summary>
    private void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        CameraFollow();
    }

    /// <summary>
    /// The camera follows the character
    /// </summary>
    public void CameraFollow()
    {
        var playerPosition = playerTransform.position;
        _transform.position = new Vector3(playerPosition.x + offset.x, playerPosition.y+ offset.y, _transform.position.z);
    }
}