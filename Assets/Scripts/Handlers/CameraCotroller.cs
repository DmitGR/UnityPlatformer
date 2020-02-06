﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;

    }

    private void Update()
    {
        Vector3 position = target.position;
        position.z -= 10.0f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}
