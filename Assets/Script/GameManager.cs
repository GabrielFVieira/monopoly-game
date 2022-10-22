using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BaseTile[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        if (tiles != null) {
            for (int i = 0; i < tiles.Length; i++) {
                tiles[i].SetId(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player GetCurrentPlayer() {
        Player player = new Player();
        player.SetId(1);
        player.SetColor(new Color(0, 0, 0));

        return player;
    }

    public int GetPlayerCurPosition(int playerId) {
        if (playerId == 1) {
            return 2;
        }

        return 0;
    }
}
