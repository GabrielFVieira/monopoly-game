using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BaseTile[] tiles;

    [SerializeField]
    private Player[] players;

    // Start is called before the first frame update
    void Start()
    {
        if (tiles != null) {
            for (int i = 0; i < tiles.Length; i++) {
                tiles[i].SetId(i);
            }
        }

        if (players != null) {
            for (int i = 0; i < players.Length; i++) {
                players[i].SetId(i + 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BaseTile[] GetTiles() {
        return tiles;
    }

    public Player GetCurrentPlayer() {
        return players[0];
    }

    public int GetPlayerCurPosition(int playerId) {
        if (playerId == 1) {
            return 1;
        }

        return 0;
    }
}
