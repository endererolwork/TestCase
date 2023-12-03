using UnityEngine;

namespace AlictusPlatform {
    public interface ISpawnPointStrategy {
        Transform NextSpawnPoint();
    }
}