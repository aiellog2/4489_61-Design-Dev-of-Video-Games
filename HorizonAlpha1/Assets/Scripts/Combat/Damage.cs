using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Damage : MonoBehaviour
  {
      [SerializeField] private Collider playerCollider;

      private float damage;
      private float multiplier;
      private float knockback;
      public SpriteRenderer sprite;

      private List<Collider> CollidedWith = new List<Collider>();

      public void Start()
      {
        //multiplier = GetComponent<BaseStats>().GetStat(Stat.Damage);

      }

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

          if(other.TryGetComponent<Force>(out Force force))
          {
            Vector3 direction = ((other.transform.position - playerCollider.transform.position).normalized);
            force.AddForce(direction * knockback);
          }
      }
      public void SetAttack(int damage, float knockback)
      {
          this.damage = damage; // * multiplier;
          this.knockback = knockback;
      }
  }
