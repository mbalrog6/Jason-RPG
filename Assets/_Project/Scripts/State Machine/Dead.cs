using UnityEngine;

namespace JasonRPG
{
    public class Dead : IState
    {
        const float DESPAWN_DELAY = 5f;
        
        private readonly Entity.Entity _entity;
        private float _despawnTime;

        public Dead(Entity.Entity entity)
        {
            _entity = entity;
        }

        public void Tick()
        {
            if (Time.time >= _despawnTime)
            {
                GameObject.Destroy(_entity.gameObject);
            }
        }

        public void OnEnter()
        {
            // Drop Loot
            _despawnTime = Time.time + DESPAWN_DELAY;
        }

        public void OnExit()
        {
        }
    }
}