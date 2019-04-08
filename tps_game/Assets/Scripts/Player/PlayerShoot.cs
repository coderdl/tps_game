using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{


    Shooter[] weapons;
    int currentWeaponIndex;
    Shooter activeWeapon;
    [SerializeField] Shooter assaultRifle;

    [SerializeField] float weaponSwitchTime;

    bool canFire;

    public Shooter ActiveWeapon
    {
        get
        {
            return activeWeapon;
        }
    }

    private void Awake()
    {
        
        //canFire = true;
        //weapons = transform.Find("Weapons").GetComponentsInChildren<Shooter>();
        //if (weapons.Length > 0)
        //   Equip(0);
    }

    void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex += direction;

        if (currentWeaponIndex > weapons.Length - 1)
            currentWeaponIndex = 0;

        if (currentWeaponIndex < 0)
            currentWeaponIndex = weapons.Length - 1;

        GameManager.Instance.Timer.Add(() =>
        {
            Equip(currentWeaponIndex);
        }, weaponSwitchTime);

        
    }

    void Equip(int index)
    {
        canFire = true;
        activeWeapon = weapons[index];
        DeactivateWeapons();
        weapons[index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.InputController.Fire1)
        {
            if (assaultRifle.Fire())
            {
                GameManager.Instance.SocketController.sendData("{\"type\":\"fire\", \"gameId\":" + GameManager.GameId + ",\"playerId\":\"" + GameManager.PlayerId + "\"}");
            }
        }
        //if (GameManager.Instance.InputController.MouseWheelDown)
        //{
        //    SwitchWeapon(1);
        //}
        //if (GameManager.Instance.InputController.MouseWheelUp)
        //{
        //    SwitchWeapon(-1);
        //}
        //if (!canFire)
        //    return;
    }

    private void OnEnable()
    {
        
    }

    void DeactivateWeapons()
    {
        for(int i = 0; i<weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }
}
