using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketMarineProjectileHandler : MonoBehaviour
{
    public Button ShootButton;

    private void Awake()
    {

        if (!ShootButton)
        {
            Debug.LogWarning("No ShootButton binded!");
        }
        else
        {
            ShootButton.onClick.AddListener(DoRayCast);
        }
    }
    
    private void DoRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity);
        Debug.DrawRay(transform.position, transform.right, Color.green, 1f);
    }
}
