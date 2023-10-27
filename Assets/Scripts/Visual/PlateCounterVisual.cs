using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter parent;
    [SerializeField] private GameObject counterTopPoint;
    [SerializeField] private float plateOfset = 0.1f;
    private KitchenSO kitchenSO;
    private List<GameObject> plateObjectList = new();

    private void Start() {
        kitchenSO = parent.GetKitchenSO();
        parent.OnPlatePickUp += OnPlatePickUp;
        parent.OnPlateSpawn += OnPlateSpawn;
    }
    private void OnPlatePickUp(object sender,EventArgs eventArgs){
        GameObject lastPlate = plateObjectList.Last();
        plateObjectList.Remove(lastPlate);
        Destroy(lastPlate);
    }
    private void OnPlateSpawn(object sender,EventArgs eventArgs){
        GameObject plate = Instantiate(kitchenSO.prefab.gameObject,counterTopPoint.transform);
        plate.transform.localPosition = new Vector3(0,plateObjectList.Count * plateOfset,0);
        plateObjectList.Add(plate);
    }
}
