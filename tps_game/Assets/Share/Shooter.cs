using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;

    [HideInInspector]
    public Transform muzzle;

    private WeaponReloader reloader;

    float nextFireAllowed;
    public bool canFire;

    void Start()
    {
        muzzle = transform.Find("Muzzle");
    }

    public void Reload()
    {
        if (reloader == null)
            return;
        reloader.Reload();
    }
    private void Awake()
    {
        muzzle = transform.Find("Muzzle");

        reloader = GetComponent<WeaponReloader>();
    }

    public virtual bool Fire()
    {
        muzzle = transform.Find("Muzzle");
        canFire = false;
        if(Time.time < nextFireAllowed)
        {
            return false;
        }

        if(reloader != null)
        {
            if (reloader.IsReloading)
                return false;

            if (reloader.RoundsRemainingInClip == 0)
            {
                return false;
            }

            reloader.TakeFromClip(1);
        }
        nextFireAllowed = Time.time + rateOfFire;

        Instantiate(projectile, muzzle.position, muzzle.rotation);

        canFire = true;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
