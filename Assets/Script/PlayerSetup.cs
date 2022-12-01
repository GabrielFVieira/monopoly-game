using UnityEngine;
using UnityEngine.UI;

public class PlayerSetup : MonoBehaviour {
    [SerializeField] public int playerIndex;
    public static int readyPlayersAmount = 0;

    public Toggle[] toggles;
    public PlayerSetup[] otherPlayers;

    public void SetPlayerInitiative(int initiative) {
        MainMenuManager.players[playerIndex].Initiative = initiative;
    }

    public void SetPlayerPiece(int piece)
    {
        if (toggles.Length < piece + 1) {
            return;
        }

        Toggle toggle = toggles[piece];

        if (!toggle.interactable) {
            return;
        }

        if (MainMenuManager.players.Count < playerIndex + 1 || MainMenuManager.players[playerIndex] == null) {
            return;
        }

        PlayerColor oldColor = MainMenuManager.players[playerIndex].Color;

        if (!toggle.isOn && oldColor != PlayerColor.NONE) {
            ToggleOtherPieces((int)oldColor - 1, true);
            MainMenuManager.players[playerIndex].Color = PlayerColor.NONE;
            return;
        }

        PlayerColor color = (PlayerColor)piece+1;

        if (oldColor == color) {
            return;
        }

        MainMenuManager.players[playerIndex].Color = color;

        ToggleOtherPieces(piece, false);

        if (oldColor != PlayerColor.NONE) {
            ToggleOtherPieces((int)oldColor-1, true);
        }
    }

    public void TogglePiece(int piece, bool enabled) {
        Toggle toggle = toggles[piece];
        toggle.interactable = enabled;
    }

    private void ToggleOtherPieces(int piece, bool enabled) {
        if (otherPlayers == null) {
            return;
        }

        foreach (PlayerSetup p in otherPlayers) {
            p.TogglePiece(piece, enabled);
        }
    }

    public void SetPlayerName(string name){
        MainMenuManager.players[playerIndex].Name = name;        
    }

    public void SetAI() {
        MainMenuManager.players[playerIndex].AI = !MainMenuManager.players[playerIndex].AI;
    }

    public void SetReady() {
        MainMenuManager.readyPlayersAmount++;
        if (MainMenuManager.readyPlayersAmount == MainMenuManager.players.Count) {
            MainMenuManager.LoadGame();
        }
    }
}