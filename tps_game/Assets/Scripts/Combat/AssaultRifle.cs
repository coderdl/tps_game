using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Shooter
{
    public override bool Fire()
    {
        base.Fire();

        if (canFire)
        {
            return true;
        }

        return false;
    }

    public void Update()
    {
        if (GameManager.Instance.InputController.Reload)
        {
            Reload();
        }
    }

}
