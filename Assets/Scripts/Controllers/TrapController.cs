using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapController : MonoBehaviour
{
    public Transform SpawnTransform;
    public Transform MoveTowardsTransform;

    /// <summary>
    /// Called when the trap reaches it's bound
    /// </summary>
    public UnityEvent OnBoundsReached;

    private bool _isMove;

    private void Awake()
    {
        gameObject.SetActive(false);
        _isMove = true;
        OnBoundsReached = new UnityEvent();
    }

    private void Start()
    {   
        SpawnTransform.position = new Vector3(SpawnTransform.position.x, MoveTowardsTransform.position.y, SpawnTransform.position.z);
        transform.position = SpawnTransform.position;
        RunningManMovementController.OnPlayerDeath.AddListener(StopSelf);
        
    }

    private void OnDestroy()
    {
        OnBoundsReached.RemoveAllListeners();
        RunningManMovementController.OnPlayerDeath.RemoveListener(StopSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMove == true)
        {
            var newPos = Vector3.MoveTowards(transform.position, MoveTowardsTransform.position, 1.5f * Time.deltaTime);
    
            transform.position = newPos;
        
            if(transform.position == MoveTowardsTransform.position)
            {
                OnBoundsReached.Invoke();
                gameObject.SetActive(false);
                transform.position = SpawnTransform.position;
            }
        }
    }

    private void StopSelf()
    {
        _isMove = false;
    }

}
