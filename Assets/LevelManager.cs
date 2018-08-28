using System.Collections.Generic;
using BGJ2018;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    internal int CurrentLevel { get; private set; }

    [SerializeField] private List<Level> test;

    internal void LevelUp()
    {
        CurrentLevel++;
    }
}

[System.Serializable]
internal class Level
{
    internal List<Transform> gameObjectToReset;
    internal List<IlluminatableObject> illuminatablesToPutOut;
}
