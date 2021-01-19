using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingSprite : MonoBehaviour
{
    [Tooltip("How fast to scroll the texture")]
    public float ScrollSpeed;

    private Vector2 _textureOffset;
    private Renderer _ren;
    private bool _shouldScroll;

    private void Awake()
    {
        if (GetComponent<Renderer>() != null)
        {
            _ren = GetComponent<Renderer>();
            _textureOffset = _ren.material.mainTextureOffset;
        }
        else
        {
            Debug.LogWarning("Failed to get Renderer on" + gameObject.name);
        }

        _shouldScroll = true;
    }

    private void Start()
    {
        RunningManMovementController.OnPlayerDeath.AddListener(StopScrolling);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerDeath.RemoveListener(StopScrolling);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_shouldScroll == true)
        {
            var x = Mathf.Repeat(Time.time * ScrollSpeed, 1);
            var offset = new Vector2(x, _textureOffset.x);
            _ren.material.mainTextureOffset = offset;
        }
    }

    private void StopScrolling()
    {
        _shouldScroll = false;
    }
}
