using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTreadmillGenerator : MonoBehaviour
{
    [Tooltip("The point in 2D space where the treadmill begins")]
    public Transform TreadmillStartPosition;

    [Tooltip("The number of tiles to spawn for the entire treadmill")]
    public int NumOfTilesToSpawn;

    public LevelTile TileDirt;

    private void Awake()
    {
        if(!TreadmillStartPosition)
        {
            Debug.LogWarning("TileTreadmillGenerator has no start position set!");
        }

        if (NumOfTilesToSpawn <= 0)
        {
            Debug.LogWarning("TileTreadmillGenerator has tile spawn value of less than 0!");
        }

    }
}
