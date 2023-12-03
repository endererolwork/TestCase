using UnityEngine;

namespace AlictusPlatform {
    public interface IEntityFactory<T> where T : Entity {
        T Create(Transform spawnPoint);
    }
}