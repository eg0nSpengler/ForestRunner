using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class RunningManAudioController : MonoBehaviour
{
    [Header("Audio Banks")]
    public AudioBank StepAudioBank;
    public AudioBank JumpAudioBank;

    public AudioClip DeathSound;

    private AudioSource _audioSrc;

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.playOnAwake = false;
        _audioSrc.loop = false;
    }

    private void Start()
    {
        RunningManMovementController.OnPlayerJump.AddListener(PlayJumpAudio);
        RunningManMovementController.OnPlayerDeath.AddListener(PlayDeathAudio);
        StartCoroutine(HandleRunAudio());
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerJump.RemoveListener(PlayJumpAudio);
        RunningManMovementController.OnPlayerDeath.RemoveListener(PlayDeathAudio);

        StopAllCoroutines();
    }

    private void PlayJumpAudio()
    {
        var rand = Random.Range(0, JumpAudioBank.AudioClips.Count);
        _audioSrc.clip = JumpAudioBank.AudioClips[rand];
        _audioSrc.Play();
    }

    private void PlayDeathAudio()
    {
        _audioSrc.clip = DeathSound;
        _audioSrc.Play();
    }

    private IEnumerator HandleRunAudio()
    {
        yield return new WaitUntil(() => RunningManMovementController.PlayerState == RunningManMovementController.PLAYER_STATE.RUNNING);

        while(RunningManMovementController.PlayerState == RunningManMovementController.PLAYER_STATE.RUNNING)
        {
            var rand = Random.Range(0, StepAudioBank.AudioClips.Count);
            _audioSrc.clip = StepAudioBank.AudioClips[rand];
            _audioSrc.Play();
            yield return new WaitForSeconds(0.3f);
            if (RunningManMovementController.PlayerState != RunningManMovementController.PLAYER_STATE.RUNNING)
            {
                break;
            }
        }

        StartCoroutine(HandleRunAudio());
        
    }

}
