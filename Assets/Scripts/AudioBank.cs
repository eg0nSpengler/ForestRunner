using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Audio Data", menuName ="Scriptable Objects/Audio Bank", order = 1)]
public class AudioBank : ScriptableObject
{
    public List<AudioClip> AudioClips = new List<AudioClip>();
}
