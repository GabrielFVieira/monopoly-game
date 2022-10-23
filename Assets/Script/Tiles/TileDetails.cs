
using UnityEngine;

[System.Serializable]
public class TileDetails {
    [SerializeField]
    private int rent;

    [SerializeField]
    private int house1;

    [SerializeField]
    private int house2;

    [SerializeField]
    private int house3;

    [SerializeField]
    private int house4;

    [SerializeField]
    private int hotel;

    [SerializeField]
    private int housePurchaseValue;

    [SerializeField]
    private int hotelPurchaseValue;

    [SerializeField]
    private int mortgageValue;

    [SerializeField]
    private Player owner;

    public int GetRent() {
        return rent;
    }

    public int GetHouse1() {
        return house1;
    }

    public int GetHouse2() {
        return house2;
    }

    public int GetHouse3() {
        return house3;
    }

    public int GetHouse4() {
        return house4;
    }

    public int GetHotel() {
        return hotel;
    }

    public Player GetOwner() {
        return owner;
    }

    public void SetOwner(Player newOwner) {
        owner = newOwner;
    }
}