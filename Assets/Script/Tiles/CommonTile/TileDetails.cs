
using UnityEngine;

[System.Serializable]
public class TileDetails {

    [field: SerializeField]
    public int Rent { get; private set; }

    [field: SerializeField]
    public int House1 { get; private set; }

    [field: SerializeField]
    public int House2 { get; private set; }

    [field: SerializeField]
    public int House3 { get; private set; }

    [field: SerializeField]
    public int House4 { get; private set; }

    [field: SerializeField]
    public int Hotel { get; private set; }

    [field: SerializeField]
    public int HousePurchaseValue { get; private set; }

    [field: SerializeField]
    public int HotelPurchaseValue { get; private set; }

    [field: SerializeField]
    public int MortgageValue { get; private set; }
}