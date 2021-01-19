using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    [Header("Text Mesh Pro References")]
    public TextMeshProUGUI VelocityText;
    public TextMeshProUGUI AnimStateText;
    public TextMeshProUGUI PlayerStateText;

    private void Awake()
    {
        if (!VelocityText || !AnimStateText || !PlayerStateText)
        {
            Debug.LogWarning(gameObject.name + " is missing a reference!");
        }

        VelocityText.text = " ";
        AnimStateText.text = " ";
        PlayerStateText.text = " ";
    }

    private void Update()
    {
        AnimStateText.text = "Current Animation Controller: " + AnimationHandler.CurrentAnimationController;
        VelocityText.text = "Current Y Velocity: " + RunningManMovementController.CurrentVelocity.ToString();
        PlayerStateText.text = "Current Player State: " + RunningManMovementController.PlayerState.ToString();
    }

}
