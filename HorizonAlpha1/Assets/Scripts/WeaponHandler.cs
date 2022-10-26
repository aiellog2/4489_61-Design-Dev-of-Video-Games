using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponlogic;

    public void EnableWeapon()
    {
        weaponlogic.SetActive(true);
    }

    public void DisableWeapon()
    {
        weaponlogic.SetActive(false);
    }
}
