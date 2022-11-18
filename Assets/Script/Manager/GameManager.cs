using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BaseTile[] tiles;
    [SerializeField]
    private Player[] players;
    [SerializeField]
    private List<Transform> playersInitialPositions = new(4);

    [SerializeField]
    private CurrencyPanel currencyPanel;
    [SerializeField]
    private PositionIndicators positionIndicators;
    [SerializeField]
    private Dice dice;

    [SerializeField] private int playerInitialMoney = 2558000;
    [SerializeField] private int curPlayerIndex = 0;

    private int playerInitialSortOrder;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject passTurnBtn;

    // Start is called before the first frame update
    void Start()
    {
        playerInitialSortOrder = playerPrefab.GetComponent<SpriteRenderer>().sortingOrder;

        InitTiles();
        InitPlayers();
        UpdatedCurrencyPanel();
        positionIndicators.SetupIndicators(players);
        StartRound();
    }

    private void UpdatedCurrencyPanel() {
        foreach (Player player in players) {
            currencyPanel.UpdateCurrency(player);
        }
    }

    private void InitTiles() {
        for (int i = 0; i < tiles.Length; i++) {
            tiles[i].SetId(i);
        }
    }

    private void InitPlayers() {
        List<PlayerChoice> choices = MainMenuManager.players;

        if (choices == null || choices.Count == 0) {
            choices = PlayersMock();
        }

        choices.Sort((x, y) => y.Initiative.CompareTo(x.Initiative));

        players = new Player[choices.Count];
        for (int i = 0; i < choices.Count; i++) {
            PlayerChoice choice = choices[i];
            
            if (!choice.AI) {
                players[i] = NewHumanPlayer(i, choice);
            }
        }
    }

    private Player NewHumanPlayer(int index, PlayerChoice choice) {
        GameObject player = Instantiate(playerPrefab, playersInitialPositions[index].position, Quaternion.identity);
        player.gameObject.name = GetPlayerName(choice.Color);

        Player playerScript = player.GetComponent<Player>();
        playerScript.SetupPlayer(index, choice.Name, playerInitialMoney, choice.Color, tiles);

        return playerScript;
    }

    private string GetPlayerName(PlayerColor color) {
        return "Player (" + color + ")";
    }

    private List<PlayerChoice> PlayersMock() {
        List<PlayerChoice> choices = new();
        PlayerChoice choice1 = new();
        choice1.Name = "test";
        choice1.Color = PlayerColor.BLUE;
        choice1.Initiative = Random.Range(1, 6);

        choices.Add(choice1);

        PlayerChoice choice2 = new();
        choice2.Name = "test 2";
        choice2.Color = PlayerColor.YELLOW;
        choice2.Initiative = Random.Range(1, 6);

        choices.Add(choice2);

        PlayerChoice choice3 = new();
        choice3.Name = "test 3";
        choice3.Color = PlayerColor.GREEN;
        choice3.Initiative = Random.Range(1, 6);

        choices.Add(choice3);

        PlayerChoice choice4 = new();
        choice4.Name = "test 4";
        choice4.Color = PlayerColor.RED;
        choice4.Initiative = Random.Range(1, 6);

        choices.Add(choice4);

        return choices;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Improve this logic
        Player player = GetCurrentPlayer();
        if (!player.IsMoving && !dice.GetEnabled()) {
            if (!passTurnBtn.activeSelf) {
                passTurnBtn.SetActive(true);
            }
        } else if (passTurnBtn.activeSelf) {
            passTurnBtn.SetActive(false);
        }
    }

    public Player GetCurrentPlayer() {
        return players[curPlayerIndex];
    }

    private void NextPlayer() {
        curPlayerIndex++;
        if (curPlayerIndex >= players.Length) {
            curPlayerIndex = 0;
        }
    }

    public void PassTurn() {
        GetCurrentPlayer().GetComponent<SpriteRenderer>().sortingOrder = playerInitialSortOrder;

        NextPlayer();
        StartRound();
    }

    private void StartRound() {
        positionIndicators.HighlightIndicator(curPlayerIndex);
        GetCurrentPlayer().GetComponent<SpriteRenderer>().sortingOrder += 1;

        dice.SetEnabled(true);
    }

    public void DiceRollCallback(int result) {
        Player player = GetCurrentPlayer();
        player.MovePosition(result);
    }
}
