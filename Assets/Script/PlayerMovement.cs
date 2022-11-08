using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private BoardManager boardManager;

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public void StartMoveJogador(int dado) {
        StartCoroutine(MoveJogador(dado));
    }

    public IEnumerator MoveJogador(int dado) {
        BaseTile[] tiles = gameManager.GetTiles();
        var player = gameManager.GetCurrentPlayer();
        var piece = gameManager.GetPlayerPiece(gameManager.GetCurrentPlayer());

        for (int i = 1; i <= dado; i++)
        {
            if(player.Position >= tiles.Length - 1) {
                player.Position = 0;
                piece.transform.position = tiles[0].GetWaypoint().position;
            } else { 
                piece.transform.position = tiles[player.Position +1].GetWaypoint().position;
                player.Position++;
            }

            yield return new WaitForSeconds(0.5f);

        }
        gameManager.NextPlayer();
        gameManager.HighlightCurrentPlayer();
    }
}
