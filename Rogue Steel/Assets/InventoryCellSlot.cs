using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCellSlot : MonoBehaviour
{
    public string type;
    public string moniker;
    public int amount;
    public void SetValues(string type, string moniker, int amount)
    {
        this.type = type;
        this.moniker = moniker;
        this.amount = amount;
    }
}
