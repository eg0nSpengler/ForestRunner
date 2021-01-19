using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrapHandler : MonoBehaviour
{
    private Transform _transform;
    private readonly int _minTreeHeight = 2;
    private readonly int _maxTreeHeight = 16;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        var randHeight = Random.Range(_minTreeHeight, _maxTreeHeight);
        _transform.localScale = new Vector3(1, randHeight, 1);
    }
}
