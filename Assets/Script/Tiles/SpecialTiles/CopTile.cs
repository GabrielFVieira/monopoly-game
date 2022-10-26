using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopTile : BaseTile {
    public override void ExecuteAction(Player player) {
        Debug.Log("Executing Cop action");
    }
}
