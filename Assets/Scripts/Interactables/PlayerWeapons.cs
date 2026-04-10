using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public GameObject sword;
    public GameObject gun;

    void Start()
    {
        sword.SetActive(false);
        gun.SetActive(false);
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