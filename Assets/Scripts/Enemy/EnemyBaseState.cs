﻿using UnityEngine;

namespace AlictusPlatform {
    public abstract class EnemyBaseState : IState {
        protected readonly Enemy enemy;
        protected readonly Animator animator;
        
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int RunHash = Animator.StringToHash("Run");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");
        protected static readonly int DieHash = Animator.StringToHash("Death");
        
        protected const float crossFadeDuration = 0.1f;
        

        protected EnemyBaseState(Enemy enemy, Animator animator) {
            this.enemy = enemy;
            this.animator = animator;
        }
        
        public virtual void OnEnter() {
            // noop
        }

        public virtual void Update() {
            // noop
        }

        public virtual void FixedUpdate() {
            // noop
        }

        public virtual void OnExit() {
            // noop
        }
    }
}