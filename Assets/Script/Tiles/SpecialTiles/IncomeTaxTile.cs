using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeTaxTile : BaseTile
{
    [SerializeField] private int valueToReceive = 2000;

    public override void ExecuteAction(Player player) {
        Debug.Log("Executing Income Tax Tile action");
        Debug.Log("Adding " + Utils.FormatPrice(valueToReceive) + " to the player " + player.Name);

        player.Receive(valueToReceive);
    }
}
