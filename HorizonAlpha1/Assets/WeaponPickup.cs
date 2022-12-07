using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weapon = null;

    private void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
        other.GetComponent<PlayerStateMachine>().EquipWeapon(weapon);
        Destroy(gameObject);
      }
    }
}
