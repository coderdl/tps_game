using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{

    [SerializeField] int maxAmmo;
    [SerializeField] float reloadTime;
    [SerializeField] public int clipSize;
    [SerializeField] Container inventory;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    private void Awake()
    {
         containerItemId = inventory.Add(this.name, maxAmmo);
    }

    public int RoundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }
        isReloading = true;
        // int amountFromInventory = inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip);

        GameManager.Instance.Timer.Add(() =>
        {
            ExcuteReload(inventory.TakeFromContainer(containerItemId, clipSize - RoundsRemainingInClip));
        },reloadTime);
    }

    private void ExcuteReload(int amount)
    {
        isReloading = false;

        shotsFiredInClip -= amount;

        GameManager.Instance.SocketController.sendData("{type:reload, gameId:" + GameManager.GameId + "}");
    }

    public void TakeFromClip(int amount)
    {
        shotsFiredInClip += amount;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
