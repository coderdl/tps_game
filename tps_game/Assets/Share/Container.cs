using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Container : MonoBehaviour
{
    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maxinum;

        private int amountTaken;

        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }

        public int Remaining
        {
            get
            {
                return Maxinum - amountTaken;
            }
        }


        public int Get(int value)
        {
            if(amountTaken + value > Maxinum)
            {
                int toMuch = amountTaken + value - Maxinum;
                amountTaken = Maxinum;
                return value - toMuch;
            }

            amountTaken += value;
            return value;
        }
    }

    public List<ContainerItem> items;

    private void Awake()
    {
        items = new List<ContainerItem>();
    }

    public System.Guid Add(string nume, int maxinum)
    {
        items.Add(new ContainerItem
        {
            Id = System.Guid.NewGuid(),
            Maxinum = maxinum,
            Name = name
        });

        return items.Last().Id;
    }

    public int TakeFromContainer(System.Guid id, int amount)
    {
        var containerItem = items.Where(x => x.Id == id).FirstOrDefault();
        if(containerItem == null)
        {
            return -1;
        }

        return containerItem.Get(amount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
