using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlatePickUp;
    [SerializeField] private float plateSpawnTime = 3;
    [SerializeField] private float maxPlateAmount = 3;
    private float plateSpawnCount = 0;
    private int plateAmount = 0;
    private void Update() {
        if (plateAmount > maxPlateAmount) return;
        plateSpawnCount += Time.deltaTime;
        if (plateSpawnCount > plateSpawnTime){
            plateSpawnCount = 0;
            OnPlateSpawn?.Invoke(this,EventArgs.Empty);
            plateAmount++;
        }
    }
    public override void Interact(Player player)
    {
        if ( !player.HasKitchenObject() && plateAmount > 0){
            plateAmount --;
            OnPlatePickUp?.Invoke(this,EventArgs.Empty);
            KitchenObject kitchenObject = Instantiate(kitchenSO.prefab).GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
}
