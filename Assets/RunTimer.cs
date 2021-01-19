using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RunTimer : MonoBehaviour
{
    public Transform FinalTimePosition;

    [Tooltip("The font size to use when presenting the final time to the player")]
    public int FinalTimeFontSize;

    private TextMeshProUGUI _tm;
    private bool _shouldUpdate;

    private void Awake()
    {
        _tm = GetComponent<TextMeshProUGUI>();
        _tm.text = " ";
        _shouldUpdate = true;
    }

    private void Start()
    {
        RunningManMovementController.OnPlayerDeath.AddListener(StopTimer);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerDeath.RemoveListener(StopTimer);
        _tm.text = " ";
    }

    private void Update()
    {
        if (_shouldUpdate == true)
        {
            _tm.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString() + " second(s)";
        }
    }

    private void StopTimer()
    {
        _shouldUpdate = false;
        _tm.fontSize = FinalTimeFontSize;
        _tm.transform.position = FinalTimePosition.position;
    }
}
