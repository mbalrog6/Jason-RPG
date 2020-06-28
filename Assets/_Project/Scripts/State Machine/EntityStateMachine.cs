using System;
using UnityEngine;
using UnityEngine.AI;

namespace JasonRPG
{
    public class EntityStateMachine : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private Entity.Entity _entity;
        public event Action<IState> OnEntityStateChanged;

        public Type CurrentStateType => _stateMachine.CurrentState.GetType();

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _entity = GetComponent<Entity.Entity>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new StateMachine();
            _stateMachine.OnStateChanged += state => OnEntityStateChanged?.Invoke(state);

            var idle = new Idle();
            var chasePlayer = new ChasePlayer(_navMeshAgent, player);
            var attack = new Attack();
            var dead = new Dead(_entity);

            _stateMachine.AddTransition(idle, chasePlayer, ()=> Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) >2f );
            _stateMachine.AddTransition(chasePlayer, attack, ()=> Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 2f );
            _stateMachine.AddTransition( attack, chasePlayer, ()=> Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) >2f );
            _stateMachine.AddAnyTransition(dead, () =>_entity.Health <= 0 );

            _stateMachine.SetState(idle);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}