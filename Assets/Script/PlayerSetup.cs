using UnityEngine;


public class PlayerSetup : MonoBehaviour {
    [SerializeField] public int playerIndex;
    public static int readyPlayersAmount = 0;

    public void SetPlayerInitiative(int initiative) {
        //MenuPrincipalManager.players[playerIndex].Initiative = initiative;
    }

    public void SetPlayerPiece(string piece)
    {
        //MenuPrincipalManager.players[playerIndex].Piece = piece;
    }

    public void SetPlayerName(string name){
        //MenuPrincipalManager.players[playerIndex].Name = name;
    }

    public void SetReady() {
        //MenuPrincipalManager.players[playerIndex].Ready = true;
        readyPlayersAmount++;
        if(readyPlayersAmount == MenuPrincipalManager.playerAmount) {
            MenuPrincipalManager.LoadGame();
        }
    }
}