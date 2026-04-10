using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public enum WeaponType
    {
        Sword,Gun
    }

    public WeaponType weaponType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerWeapons playerWeapons = other.GetComponent<PlayerWeapons>();

            if (playerWeapons != null)
            {
                if (weaponType == WeaponType.Sword)
                {
                    playerWeapons.EnableSword();
                }
                else if (weaponType == WeaponType.Gun)
                {
                    playerWeapons.EnableGun();
                }
            }
            Destroy(gameObject);
        }
    }
}