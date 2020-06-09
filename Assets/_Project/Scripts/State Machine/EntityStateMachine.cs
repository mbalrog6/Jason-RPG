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

        public Type CurrentStateType => _stateMachine.CurrentState.GetType();

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            _entity = GetComponent<Entity.Entity>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new StateMachine();

            var idle = new Idle();
            var chasePlayer = new ChasePlayer(_navMeshAgent);
            var attack = new Attack();
            var dead = new Dead();

            _stateMachine.Add(idle);
            _stateMachine.Add(chasePlayer);
            _stateMachine.Add(attack);
            _stateMachine.Add(dead);
            
            _stateMachine.AddTransition(idle, chasePlayer, ()=> Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) >2f );
            _stateMachine.AddTransition(chasePlayer, attack, ()=> Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 2f );
            _stateMachine.AddAnyTransition(dead, () =>_entity.Health <= 0 );

            _stateMachine.SetState(idle);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }
    }
}