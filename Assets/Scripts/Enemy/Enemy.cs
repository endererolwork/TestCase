using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace AlictusPlatform
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public class Enemy : Entity
    {
        [SerializeField] NavMeshAgent agent;
        [SerializeField] PlayerDetector playerDetector;
        [SerializeField] Animator animator;

        [SerializeField] float wanderRadius = 10f;
        [SerializeField] float timeBetweenAttacks = 1f;

        [SerializeField] private float projectileForce = 10f;
        [SerializeField] private float projectileLifetime = 5f;
        [SerializeField] private float projectileSpawnDistance = 2f; 


        StateMachine stateMachine;

        CountdownTimer attackTimer;

        [SerializeField] private GameObject rangedProjectilePrefab;

        void Start()
        {
            attackTimer = new CountdownTimer(timeBetweenAttacks);

            stateMachine = new StateMachine();

            var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius);
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player);
            var attackState = new EnemyAttackState(this, animator, agent, playerDetector.Player);

            At(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
            At(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

            stateMachine.SetState(wanderState);
        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        void Update()
        {
            stateMachine.Update();
            attackTimer.Tick(Time.deltaTime);
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        public void Attack()
        {
            if (attackTimer.IsRunning) return;

            attackTimer.Start();

            // Calculate the position to instantiate the projectile
            Vector3 instantiatePosition = transform.position + transform.forward * projectileSpawnDistance;

            // Instantiate the projectile at the calculated position
            GameObject projectile = Instantiate(rangedProjectilePrefab, instantiatePosition, Quaternion.identity);

            // Calculate the direction towards the player
            Vector3 direction = (playerDetector.Player.position - instantiatePosition).normalized;

            // Get the Rigidbody component and apply force
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(direction * projectileForce, ForceMode.Impulse);

            // Destroy the projectile after a certain time
            Destroy(projectile, projectileLifetime);

            // Deal damage to the player
            playerDetector.PlayerHealth.TakeDamage(10);
        }

    }
}
