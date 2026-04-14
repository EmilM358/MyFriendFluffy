using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject sword;
    public GameObject gun;

    public enum StartingWeapon
    {
        None,
        Sword,
        Gun
    }

    public StartingWeapon startingWeapon;

    void Start()
    {
        sword.SetActive(false);
        gun.SetActive(false);

        switch (startingWeapon)
        {
            case StartingWeapon.Sword:
                EnableSword();
                break;

            case StartingWeapon.Gun:
                EnableGun();
                break;

            case StartingWeapon.None:
                break;
        }
    }

    public void EnableSword()
    {
        sword.SetActive(true);
        gun.SetActive(false);
    }

    public void EnableGun()
    {
        gun.SetActive(true);
        sword.SetActive(false);
    }
}