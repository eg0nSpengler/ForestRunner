using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenFade : MonoBehaviour
{
    private Image _img;
    private bool _shouldFade;

    private void Awake()
    {
        _img = GetComponent<Image>();

        _shouldFade = false;    
    }

    // Start is called before the first frame update
    void Start()
    {
        var screenHeight = Camera.main.orthographicSize * 2f;
        var screenWidth = screenHeight * Camera.main.aspect;
        var newRect = new Rect(_img.transform.position, new Vector2(screenWidth, screenHeight));    

        RunningManMovementController.OnPlayerDeath.AddListener(DoScreenFade);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerDeath.RemoveListener(DoScreenFade);

    }
    // Update is called once per frame
    private void Update()
    {
        if(_shouldFade == true)
        {
            _img.color = Color.Lerp(_img.color, Color.red, Time.deltaTime);
        }
    }

    private void DoScreenFade()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(1.5f);
        _shouldFade = true;
    }
}
