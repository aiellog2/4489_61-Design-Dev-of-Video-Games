using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using RPG.Stats;


  public class Damage : MonoBehaviour
  {
      [SerializeField] private Collider playerCollider;

      private int damage;
      private float knockback;

      private List<Collider> CollidedWith = new List<Collider>();

      private void OnEnable()
      {
          CollidedWith.Clear();
      }

      private void OnTriggerEnter(Collider other)
      {
          if (other == playerCollider) { return; }

          if (CollidedWith.Contains(other)) { return; }

          CollidedWith.Add(other);

          if (other.TryGetComponent<Health>(out Health health))
          {
              health.DealDamage(damage);
          }
      }
      public void SetAttack(int damage)
      {
          this.damage = damage;
      }
  }
