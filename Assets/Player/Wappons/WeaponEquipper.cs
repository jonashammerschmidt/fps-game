using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Weapon
{
    GrapplingGun,
    Revolver
}

public class WeaponEquipper : MonoBehaviour
{

    public Transform playerHand;
    public Camera mainCamera;
    public Rigidbody playerBodyRigidbody;

    public GameObject grapplingGunPrefab;
    public GameObject revolverPrefab;

    private List<Weapon> weapons = new List<Weapon>() {
        Weapon.GrapplingGun,
        Weapon.Revolver
    };
    private Weapon currentWeapon = Weapon.GrapplingGun;
    private GameObject currentWeaponObject;

    void Start()
    {
        EquipWeapon(currentWeapon);
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            EquipNextWappon();
        }
    }

    public void EquipNextWappon()
    {
        if (currentWeapon == Weapon.GrapplingGun)
        {
            EquipWeapon(Weapon.Revolver);
        }
        else
        {
            EquipWeapon(Weapon.GrapplingGun);
        }
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (this.currentWeapon != weapon || currentWeaponObject == null)
        {
            if (currentWeaponObject)
            {
                Destroy(currentWeaponObject);
            }

            if (weapon == Weapon.Revolver)
            {
                this.currentWeaponObject = GameObject.Instantiate(revolverPrefab, playerHand);
            }
            else if (weapon == Weapon.GrapplingGun)
            {
                this.currentWeaponObject = GameObject.Instantiate(grapplingGunPrefab, playerHand);
                var grapplingGun = this.currentWeaponObject.GetComponent<GrapplingGun>();
                grapplingGun.cameraTransform = mainCamera.transform;
                grapplingGun.playerRigidbody = playerBodyRigidbody;

            }

            this.currentWeapon = weapon;
        }
    }

}
