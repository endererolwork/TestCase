using UnityEngine;
using Utilities;

namespace AlictusPlatform {
    public class EnemySpawnManager : EntitySpawnManager {
        [SerializeField] EnemyData[] enemyData;
        [SerializeField] float spawnInterval = 1f;
        
        EntitySpawner<Enemy> spawner;
        
        CountdownTimer spawnTimer;
        int counter;

        protected override void Awake() {
            base.Awake();

            spawner = new EntitySpawner<Enemy>(
                new EntityFactory<Enemy>(enemyData),
                spawnPointStrategy);
            
            spawnTimer = new CountdownTimer(spawnInterval);
            spawnTimer.OnTimerStop += () => {
                if (counter++ >= spawnPoints.Length) {
                    spawnTimer.Stop();
                    return;
                }
                Spawn();
                spawnTimer.Start();
            };
        }
        
        void Start() => spawnTimer.Start();
        
        void Update() => spawnTimer.Tick(Time.deltaTime);

        public override void Spawn() => spawner.Spawn();
    }
}