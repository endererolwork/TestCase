using System.Collections.Generic;
using UnityEngine;

namespace AlictusPlatform {
    public abstract class EventChannel<T> : ScriptableObject {
        readonly HashSet<EventListener<T>> observers = new();

        public void Invoke(T value) {
            if (!EqualityComparer<T>.Default.Equals(value, default(T))) {
                foreach (var observer in observers) {
                    observer.Raise(value);
                }
            }
        }

        public void Register(EventListener<T> observer) => observers.Add(observer);
        public void Deregister(EventListener<T> observer) => observers.Remove(observer);
    }

    public readonly struct Empty { }

    [CreateAssetMenu(menuName = "Events/EventChannel")]
    public class EventChannel : EventChannel<Empty> { }
}