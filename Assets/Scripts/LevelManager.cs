using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BGJ2018
{
    public class LevelManager : MonoBehaviour
    {
        internal int CurrentLevel { get; private set; }

        [SerializeField] private KeyCode reloadKey = KeyCode.R;
        [SerializeField] private List<Level> levels;

        private SceneLoader sceneLoader;

        private void Start()
        {
            foreach (var level in levels)
            {
                level.SetGameObjectStartPositions();
            }

            sceneLoader = FindObjectOfType<SceneLoader>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(reloadKey))
            {
                sceneLoader.ResetLevel(this);
            }
        }

        internal void LevelUp()
        {
            CurrentLevel++;
        }

        // Called from SceneLoader
        internal void ResetLevelObjects()
        {
            var levelToRestart = levels.FirstOrDefault(x => x.LevelNumber == CurrentLevel);

            if (levelToRestart == null) return;

            var player = FindObjectOfType<PlayerController>();

            player.transform.position = player.LatestCheckpoint;

            // Resets all the game object positions like they were in start
            for (var i = 0; i < levelToRestart.GameObjectsToReset.Count; i++)
            {
                levelToRestart.GameObjectsToReset[i].position = levelToRestart.GameObjectStartPositions[i];
            }

            foreach (var illuminatable in levelToRestart.IlluminatablesToReset)
            {
                illuminatable.ResetEnergy();
            }
        }
    }

    [System.Serializable]
    internal class Level
    {
        [SerializeField] internal int LevelNumber;
        [SerializeField] internal List<Transform> GameObjectsToReset; // Resets the position of this game object if the player hits restart
        [SerializeField] internal List<IlluminatableObject> IlluminatablesToReset;

        internal List<Vector3> GameObjectStartPositions = new List<Vector3>();

        internal void SetGameObjectStartPositions()
        {
            foreach (var gameObject in GameObjectsToReset)
            {
                GameObjectStartPositions.Add(gameObject.position);
            }
        }
    }
}
