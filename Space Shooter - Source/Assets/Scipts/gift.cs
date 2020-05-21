using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;
using Random = UnityEngine.Random;

public class gift : MonoBehaviour
{
    [Header("Prize")]
    [SerializeField] private float xPadding;
    [SerializeField] private float fallSpeed;
    
    private float x, y, bottom, movement;
    // Use this for initialization
    void Start()
    {
        Camera gameCamera = Camera.main;
        bottom = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        var xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        var xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
        y = transform.position.y;
        x = Random.Range(xMin + xPadding, xMax - xPadding);
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        y = transform.position.y;
        if (y >= bottom)
        {
            var movement = (float)fallSpeed * Time.deltaTime;
            transform.position = new Vector2(x, y + movement);
        }
        else
        {
            Clear();
        }
    }

    private void Clear()
    {
        Destroy(gameObject);
    }

}