using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class AnimationHandler : MonoBehaviour
{
    public Button JumpButton;

    /// <summary>
    /// The current Animation Controller in use
    /// </summary>
    public static string CurrentAnimationController { get; private set; }

    [Header("Animation Controllers")]
    public RuntimeAnimatorController RunAnimationController;
    public RuntimeAnimatorController IdleAnimationController;
    public RuntimeAnimatorController JumpAnimationController;
    public RuntimeAnimatorController FallAnimationController;
    public RuntimeAnimatorController DeathAnimationController;

    private Animator _animController;

    private void Awake()
    {
        _animController = GetComponent<Animator>();
    }
    private void Start()
    {
        RunningManMovementController.OnPlayerJump.AddListener(PlayJumpAnim);
        RunningManMovementController.OnPlayerFall.AddListener(PlayFallAnim);
        RunningManMovementController.OnPlayerRun.AddListener(PlayRunAnim);
        RunningManMovementController.OnPlayerDeath.AddListener(PlayDeathAnim);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerJump.RemoveListener(PlayJumpAnim);
        RunningManMovementController.OnPlayerFall.RemoveListener(PlayFallAnim);
        RunningManMovementController.OnPlayerRun.RemoveListener(PlayRunAnim);
        RunningManMovementController.OnPlayerDeath.RemoveListener(PlayDeathAnim);
    }


    private void Update()
    {
        CurrentAnimationController = _animController.runtimeAnimatorController.name;
    }

    private void PlayJumpAnim()
    {
          _animController.runtimeAnimatorController = JumpAnimationController;
    }
    private void PlayFallAnim()
    {
          _animController.runtimeAnimatorController = FallAnimationController;
    }

    private void PlayRunAnim()
    {
          _animController.runtimeAnimatorController = RunAnimationController;
    }

    private void PlayDeathAnim()
    {
        _animController.runtimeAnimatorController = DeathAnimationController;
    }
}
