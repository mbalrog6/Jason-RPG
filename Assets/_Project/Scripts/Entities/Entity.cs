using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JasonRPG.Entity
{
    public class Entity : MonoBehaviour, ITakeHit
    {
        [SerializeField] private int maxHealth = 5;
        public int Health { get; set; }

        public event Action OnDied = delegate { };

        private void OnEnable()
        {
            Health = maxHealth;
        }

        public void TakeHit(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Die();
            }
            else
            {
                HandleNonLeathalHit();
            }
        }

        private void HandleNonLeathalHit()
        {
            Debug.Log("Took non-leathal damage");
        }

        private void Die()
        {
            Debug.Log("Died");
            OnDied?.Invoke();
        }

        [ContextMenu("Take Leathal Damage")]
        private void TakeLeathalDamage()
        {
            TakeHit(Health);
        }

    }
}
