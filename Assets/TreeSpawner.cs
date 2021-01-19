using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [Tooltip("The number of traps to spawn")]
    public int NumTrapsToSpawn;

    public TrapController TreePrefab;
    public Transform SpawnTransform1;
    public Transform SpawnTransform2;
    public Transform EndTransform1;
    public Transform EndTransform2;

    private List<TrapController> _trapList;
    private List<Transform> _spawnTransformList;
    private List<Transform> _endPointTransformList;

    private void Awake()
    {
        if(!TreePrefab)
        {
            Debug.LogWarning(gameObject.name + " is missing a prefab reference!");
        }

        _trapList = new List<TrapController>();
        _spawnTransformList = new List<Transform>();
        _endPointTransformList = new List<Transform>();
    }

    private void Start()
    {
        _spawnTransformList.Add(SpawnTransform1);
        _spawnTransformList.Add(SpawnTransform2);
        _endPointTransformList.Add(EndTransform1);
        _endPointTransformList.Add(EndTransform2);

        for (var i = 0; i < NumTrapsToSpawn; i++)
        {
            _trapList.Add(Instantiate(TreePrefab));
            _trapList[i].OnBoundsReached.AddListener(ResetAndRespawn);
        }

        AssignSpawnAndEnd();

        RunningManMovementController.OnPlayerDeath.AddListener(StopSpawn);
    }

    private void OnDisable()
    {
        foreach(var trap in _trapList.GetRange(0, _trapList.Count))
        {
            trap.OnBoundsReached.RemoveListener(ResetAndRespawn);
        }

        RunningManMovementController.OnPlayerDeath.RemoveListener(StopSpawn);
    }

    private void ResetAndRespawn()
    {
        foreach(var trap in _trapList)
        {
            if(trap.gameObject.activeInHierarchy == false)
            {
                trap.gameObject.SetActive(true);
            }
        }
    }
    private void AssignSpawnAndEnd()
    {
        foreach(var trap in _trapList)
        {
            var rand = Random.Range(0, _spawnTransformList.Count);
            trap.SpawnTransform = _spawnTransformList[rand];
            trap.MoveTowardsTransform = _endPointTransformList[rand];
        }

        StartCoroutine(HandleSpawning());
    }

    private void StopSpawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator HandleSpawning()
    {
        var rand = Random.Range(0, _trapList.Count); 
        _trapList[rand].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(HandleSpawning());
    }
}
