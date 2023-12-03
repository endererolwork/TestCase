using UnityEngine;

namespace AlictusPlatform {
    public abstract class EntitySpawnManager : MonoBehaviour {
        [SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Random;
        [SerializeField] protected Transform[] spawnPoints;
        
        protected ISpawnPointStrategy spawnPointStrategy;

        protected enum SpawnPointStrategyType {
            Linear,
            Random
        }

        protected virtual void Awake() {
            spawnPointStrategy = spawnPointStrategyType switch {
                SpawnPointStrategyType.Random => new RandomSpawnPointStrategy(spawnPoints),
                _ => spawnPointStrategy
            };
        }

        public abstract void Spawn();
    }
}