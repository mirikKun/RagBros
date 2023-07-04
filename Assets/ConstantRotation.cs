using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float speed=50;
    private Transform _transform;
    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        _transform.Rotate (0,0,speed*Time.deltaTime);
    }
}
