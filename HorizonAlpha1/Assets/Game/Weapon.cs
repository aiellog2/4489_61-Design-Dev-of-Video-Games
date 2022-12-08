using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
public class Weapon : ScriptableObject
{
  [SerializeField] GameObject equippedPrefab = null;
  [SerializeField] float weaponDamage = 5f;
  //[SerializeField] float weaponRange = 2f;

  const string weaponName = "Weapon";

  public void Spawn(Transform handTransform)
  {
    //DestroyOldWeapon(handTransform);

    if (equippedPrefab != null)
    {
      GameObject weapon = Instantiate(equippedPrefab, handTransform);
      weapon.name = weaponName;
    }
  }

  /* private void DestroyOldWeapon(Transform handTransform)
  {
    Transform oldWeapon = handTransform.Find(weaponName);
    if (oldWeapon == null)
    {
      return;
    }

    oldWeapon.name = "DESTROYING";
    Destroy(oldWeapon.gameObject);
  }*/

  public float GetDamage()
  {
    return weaponDamage;
  }


}
