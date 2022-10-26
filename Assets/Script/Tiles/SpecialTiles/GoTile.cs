using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTile : BaseTile
{
    public override void ExecuteAction(Player player) {
        Debug.Log("Executing Go action");
    }
}
