using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRSTile : BaseTile
{
    [SerializeField] private int valueToPay = 2000;

    public override void ExecuteAction(Player player) {
        Debug.Log("Executing IRS Tile action");
        Debug.Log("Removing " + Utils.FormatPrice(valueToPay) + " from the player " + player.Name);

        player.Pay(valueToPay);
    }
}
