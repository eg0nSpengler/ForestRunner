using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private void Start()
    {
        RunningManMovementController.OnPlayerDeath.AddListener(HandleRestart);
    }

    private void OnDisable()
    {
        RunningManMovementController.OnPlayerDeath.RemoveListener(HandleRestart);
        StopAllCoroutines();
    }

    private void HandleRestart()
    {
        StartCoroutine(DoRestart());
    }

    private IEnumerator DoRestart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
