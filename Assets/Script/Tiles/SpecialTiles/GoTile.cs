using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTile : BaseTile
{
    [SerializeField] private int valueToReceive = 2000;

    public override void ExecuteAction(Player player) {
        Debug.Log("Executing Go action");
        Debug.Log("Adding " + Utils.FormatPrice(valueToReceive) +" to the player " + player.Name);

        player.Receive(valueToReceive);
    }
}
