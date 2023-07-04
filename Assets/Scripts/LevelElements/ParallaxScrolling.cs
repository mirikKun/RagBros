using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private float length;
    private float startPos;
    [SerializeField] private float parallaxEffect;
    [SerializeField] private Transform mainCamera;

    private Transform _transform;
    private void Start()
    {        _transform = transform;

        startPos = _transform.position.x;
    }

    private void LateUpdate()
    {
        UpdatePositions();
    }

    /// <summary>
    /// Update position of object according to parallax effect
    /// </summary>
    private void UpdatePositions()
    {
        var cameraPosition = mainCamera.position;
        float temp = cameraPosition.x * (1 - parallaxEffect);
        float dist = cameraPosition.x *  parallaxEffect;
        var position = _transform.position;
        position = new Vector3(startPos + dist, cameraPosition.y, position.z);
        _transform.position = position;
        if (temp > startPos + length)
        {
            startPos += length;
        }
    }
}
