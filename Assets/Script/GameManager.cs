using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BaseTile[] tiles;
    [SerializeField]
    public Player[] players;
    public List<Player> Players { get; set; }
    public static int CurrentPlayerIndex = 0;
    [SerializeField] private GameObject redPlayerPositionIndicator;
    [SerializeField] private GameObject bluePlayerPositionIndicator;
    [SerializeField] private GameObject greenPlayerPositionIndicator;
    [SerializeField] private GameObject yellowPlayerPositionIndicator;
    [SerializeField] private GameObject currentPlayerPositionIndicator;
    [SerializeField] private GameObject redPiece;
    [SerializeField] private GameObject bluePiece;
    [SerializeField] private GameObject greenPiece;
    [SerializeField] private GameObject yellowPiece;
    [SerializeField] private GameObject redPanel;
    [SerializeField] private GameObject bluePanel;
    [SerializeField] private GameObject greenPanel;
    [SerializeField] private GameObject yellowPanel;
    [SerializeField] private GameObject cardsIndicator;
    private List<string> piecesNames = new List<string>() { "Red", "Blue", "Green", "Yellow" };

    // Start is called before the first frame update
    void Start()
    {
        // add players if not coming from menu (for testing purposes only)
        int playerAmount = 0;
        Debug.Log("PlayerAmount: " + playerAmount);
        Debug.Log("Players: " + MenuPrincipalManager.players);
        if (MenuPrincipalManager.players == null || MenuPrincipalManager.players.Count == 0)
        {
            playerAmount = 4;
            Players = new List<Player>();
            for (int i = 0; i < playerAmount; i++)
            {
                Player player = new Player();
                player.Name = i.ToString();
                player.Piece = piecesNames[i];
                player.Initiative = Random.Range(1, 6);
                Players.Add(player);
            }
        } else {
            Players = MenuPrincipalManager.players;
            playerAmount = MenuPrincipalManager.playerAmount;
        }
        //init players in game manager
        for (int i = 0; i < playerAmount; i++) {
            Players[i].Money = 2558000;
        }
        //order game manager players by initiative
        Players.Sort((x, y) => y.Initiative.CompareTo(x.Initiative));
        //display order in board
        int j = 0;
        foreach (Player player in Players)
        {
            player.Position = 0;
            var pieceIndicator = GetPlayerPieceIndicator(player);
            pieceIndicator.transform.position += new Vector3(0.75f * j, 0, 0);
            pieceIndicator.SetActive(true);
            player.Cards = new List<Card>();
            GetPlayerPiece(player).SetActive(true);
            GetPlayerPanel(player).SetActive(true);
            UpdatePlayerPanel(player);
            if(j == 1) {
                player.Cards = new List<Card>();
                player.Cards.Add(new Card());
            }
            if(j == 2) {
                player.Cards = new List<Card>();
                player.Cards.Add(new Card());
                player.Cards.Add(new Card());
            }
            j++;
        }
        if (tiles != null) {
            for (int i = 0; i < tiles.Length; i++) {
                tiles[i].SetId(i);
            }
        }

        if (Players != null) {
            for (int i = 0; i < Players.Count; i++) {
                Players[i].SetId(i + 1);
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
        return Players[CurrentPlayerIndex];
    }

    public void NextPlayer() {
        CurrentPlayerIndex++;
        if (CurrentPlayerIndex >= Players.Count) {
            CurrentPlayerIndex = 0;
        }
    }

    public void UpdateInterface(){
        var currentPlayer = GetCurrentPlayer();
        UpdatePlayerPanel(currentPlayer);
        HighlightCurrentPlayer();
        UpdateCardsIndicatorCurrentPlayer();
    }

    public void UpdateCardsIndicatorCurrentPlayer(){
        UpdateCardsIndicator(GetCurrentPlayer());
    }

    public void UpdateCardsIndicator(Player player){
        if(player.Cards.Count > 0) {
            cardsIndicator.SetActive(true);
            cardsIndicator.transform.GetChild(0).gameObject.SetActive(true);
            cardsIndicator.transform.GetChild(1).gameObject.SetActive(player.Cards.Count > 1);
        } else {
            cardsIndicator.SetActive(false);
            cardsIndicator.transform.GetChild(0).gameObject.SetActive(false);
            cardsIndicator.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void HighlightCurrentPlayer() {
        var piece = GetPlayerPieceIndicator(GetCurrentPlayer());
        currentPlayerPositionIndicator.transform.position = piece.transform.position;
    }

    public void UpdatePlayerPanel(Player player) {
        var panel = GetPlayerPanel(player);
        var cultureInfo = CultureInfo.CreateSpecificCulture("pt-BR"); 
        var numberFormatInfo = (NumberFormatInfo)cultureInfo.NumberFormat.Clone();
        numberFormatInfo.CurrencySymbol = "";
        var formattedMoney = player.Money.ToString("C", numberFormatInfo).Replace(" ", "").Split(',')[0];
        Debug.Log(formattedMoney);
        var textObject = panel.transform.GetChild(0).gameObject;
        Debug.Log(textObject);
        var text = textObject.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        text.SetText(formattedMoney);
    }

    public GameObject GetPlayerPieceIndicator(Player player) {
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
        return piece;
    }

    public GameObject GetPlayerPiece(Player player) {
        var piece = new GameObject();
        if(player.Piece == "Green") {
            piece = greenPiece;
        }
        if(player.Piece == "Red") {
            piece = redPiece;
        }
        if(player.Piece == "Blue") {
            piece = bluePiece;
        }
        if(player.Piece == "Yellow") {
            piece = yellowPiece;
        }
        return piece;
    }

        public GameObject GetPlayerPanel(Player player) {
            var piece = new GameObject();
            if(player.Piece == "Green") {
                piece = greenPanel;
            }
            if(player.Piece == "Red") {
                piece = redPanel;
            }
            if(player.Piece == "Blue") {
                piece = bluePanel;
            }
            if(player.Piece == "Yellow") {
                piece = yellowPanel;
            }
            return piece;
    }

    public int GetPlayerCurPosition(int playerId) {
        if (playerId == 1) {
            return 1;
        }

        return 0;
    }
}
