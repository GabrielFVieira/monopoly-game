using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    [SerializeField] private GameObject redPlayerPositionIndicator;
    [SerializeField] private GameObject bluePlayerPositionIndicator;
    [SerializeField] private GameObject greenPlayerPositionIndicator;
    [SerializeField] private GameObject yellowPlayerPositionIndicator;
    public static GameManager gameManager = new GameManager();
    private List<string> piecesNames = new List<string>() { "Red", "Blue", "Green", "Yellow"};	

    public void Start() {
        gameManager.Players = MenuPrincipalManager.players;
        //init players in game manager
        for (int i = 0; i < MenuPrincipalManager.playerAmount; i++) {
            gameManager.Players[i].Money = 1000;
        }
        //order game manager players by initiative
        gameManager.Players.Sort((x, y) => y.Initiative.CompareTo(x.Initiative));
        //display order in board
        Debug.Log(gameManager.Players);
        int j = 0;
        foreach (Player player in gameManager.Players)
        {
            Debug.Log(player.Piece);
            Debug.Log(player.Initiative);
            var piece = new GameObject();
            if(player.Piece == "Green") {
                piece = greenPlayerPositionIndicator;
            }
            if(player.Piece == "Red") {
                piece = redPlayerPositionIndicator;
            }
            if(player.Piece == "Blue") {
                piece = bluePlayerPositionIndicator;
            }
            if(player.Piece == "Yellow") {
                piece = yellowPlayerPositionIndicator;
            }
            piece.transform.position += new Vector3(0.75f * j, 0, 0);
            piece.SetActive(true);
            j++;
            Debug.Log(player.Piece + " " + player.Initiative);
        }
        // var posIndicators = PositionIndicatorsGroup.GetComponentsInChildren<GameObject>();
        // foreach (Player player in gameManager.Players)
        // {
        //     var pieceName = player.Piece + "PlayerPositionIndicator";
        //     Debug.Log(posIndicators);
        //     var piece = System.Array.Find(posIndicators, x => x.name == pieceName);
        //     piece.transform.position += new Vector3(0.75f * j, 0, 0);
        //     piece.SetActive(true);
        //     j++;
        //     Debug.Log(player.Piece + " " + player.Initiative);
        // }
    }



    public void PlayerInitiative(Player player) {
        //TODO: make interactive wait for userInput
        // while(true)
        // {
        //     if(Input.GetMouseButtonDown(0))
        //     {
        //         RaycastHit hit = new RaycastHit();
        //         if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //         {
        //             if(hit.transform.gameObject.tag == "Target")
        //             {
        //                 testTransform = hit.transform;
        //                 ExitState();
        //                 Debug.Log ("I hit you");
        //                 currentState = State.Walking;
        //                 EnterState();
        //                 yield break;
        //             }
        //         }
        //     }
        //     yield return null;
        // }
        player.Initiative = Random.Range(1, 7);
    }
}