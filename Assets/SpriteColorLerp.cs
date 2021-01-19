using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorLerp : MonoBehaviour
{
    public SpriteRenderer SpriteRen1;
    public SpriteRenderer SpriteRen2;
    public SpriteRenderer SpriteRen3;
    public SpriteRenderer SpriteRen4;

    private List<SpriteRenderer> _spriteRens;
    private bool _shouldLerp;
    
    private void Awake()
    {
        _spriteRens = new List<SpriteRenderer>();
        _shouldLerp = false;
    }

    private void Start()
    {
        _spriteRens.Add(SpriteRen1);
        _spriteRens.Add(SpriteRen2);
        _spriteRens.Add(SpriteRen3);
        _spriteRens.Add(SpriteRen4);

        RunningManMovementController.OnPlayerDeath.AddListener(DoLerp);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerDeath.RemoveListener(DoLerp);
    }

    private void Update()
    {
        if (_shouldLerp == true)
        {
            foreach (var ren in _spriteRens.GetRange(0, _spriteRens.Count))
            {
                ren.color = Color.Lerp(ren.color, Color.red, Time.deltaTime);
            }
        }
    }

    private void DoLerp()
    {
        _shouldLerp = true;
    }
}
