using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : BaseCounter
{
    public static event EventHandler OnAnyTrashed;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()){
            // take object from player
            Destroy(player.GetKitchenObject().gameObject);
            OnAnyTrashed?.Invoke(this,EventArgs.Empty);
        }
    }
}
