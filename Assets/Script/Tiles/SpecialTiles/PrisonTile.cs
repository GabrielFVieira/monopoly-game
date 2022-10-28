using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonTile : BaseTile
{
    public override void ExecuteAction(Player player) {
        Debug.Log("Executing Prison action");
    }
}
