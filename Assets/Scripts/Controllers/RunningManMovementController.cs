using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class RunningManMovementController : MonoBehaviour
{
    /// <summary>
    /// The current state of the player
    /// </summary>
    public enum PLAYER_STATE
    {
        IDLE,
        DEAD,
        RUNNING,
        JUMPING,
        FALLING
    }

    [Header("References")]
    public Button JumpButton;

    [Header("Variables")]
    public float JumpVelocity;
    /// <summary>
    /// The current <b>Y</b> velocity of the player
    /// </summary>
    public static float CurrentVelocity { get; private set; }
    
    public static UnityEvent OnPlayerDeath;
    public static UnityEvent OnPlayerRun;
    public static UnityEvent OnPlayerJump;
    public static UnityEvent OnPlayerFall;

    /// <summary>
    /// The current player state
    /// </summary>
    public static PLAYER_STATE PlayerState { get; private set; }

    private Rigidbody2D _rb2d;
    private List<ContactPoint2D> _contactPoints;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _contactPoints = new List<ContactPoint2D>();
        CurrentVelocity = Mathf.Round(_rb2d.velocity.y);
        OnPlayerDeath = new UnityEvent();
        OnPlayerRun = new UnityEvent();
        OnPlayerJump = new UnityEvent();
        OnPlayerFall = new UnityEvent();
    }

    private void Start()
    {
        if (!JumpButton)
        {
            Debug.LogWarning("No JumpButton binded!");
        }
        else
        {
            EventTrigger trig = JumpButton.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
            trig.triggers.Add(entry);
        }

        PlayerState = PLAYER_STATE.FALLING;

        StartCoroutine(WaitForStateChange());

    }

    private void Update()
    {
        CurrentVelocity = Mathf.Round(_rb2d.velocity.y);
    }

    private void FixedUpdate()
    {
        while (PlayerState == PLAYER_STATE.JUMPING)
        {
            _rb2d.AddForce(Vector2.up * JumpVelocity * Time.deltaTime, ForceMode2D.Impulse);

            if (Mathf.Round(_rb2d.velocity.y) >= JumpVelocity)
            {
               PlayerState = PLAYER_STATE.FALLING;
               break;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ground1")
        {
            PlayerState = PLAYER_STATE.RUNNING;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
          PlayerState = PLAYER_STATE.DEAD;
          GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnPointerDown(PointerEventData data)
    {
        if (PlayerState != PLAYER_STATE.DEAD && PlayerState != PLAYER_STATE.JUMPING)
        {
            PlayerState = PLAYER_STATE.JUMPING;
        }
        else
        {
            return;
        }
    }

    private void HandleStateChange()
    {
        switch(PlayerState)
        {
            case PLAYER_STATE.DEAD:
                OnPlayerDeath.Invoke();
                break;
            case PLAYER_STATE.FALLING:
                OnPlayerFall.Invoke();
                break;
            case PLAYER_STATE.JUMPING:
                OnPlayerJump.Invoke();
                break;
            case PLAYER_STATE.RUNNING:
                OnPlayerRun.Invoke();
                break;

            default:
                break;

        }
    }
    private IEnumerator WaitForStateChange()
    {
        var currState = PlayerState;
        yield return new WaitUntil(() => currState != PlayerState);
        HandleStateChange();
        StartCoroutine(WaitForStateChange());
    }
}
