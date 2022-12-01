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
    private GameDice dice;

    [SerializeField] private int playerInitialMoney = 25000;
    [SerializeField] private int curPlayerIndex = 0;

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private GameObject passTurnBtn;

    // Start is called before the first frame update
    void Start()
    {
        InitTiles();
        InitPlayers();
        UpdateCurrencyPanel();
        positionIndicators.SetupIndicators(players);
        StartRound();
    }

    private void UpdateCurrencyPanel() {
        foreach (Player player in players) {
            currencyPanel.UpdateCurrency(player);
        }
    }

    public void UpdateCurrencyPanel(Player player) {
        currencyPanel.UpdateCurrency(player);
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
            players[i] = NewPlayer(i, choice);
        }
    }

    private Player NewPlayer(int index, PlayerChoice choice) {
        GameObject player = Instantiate(playerPrefab, playersInitialPositions[index].position, Quaternion.identity);
        player.gameObject.name = "Player (" + choice.Color + ")";

        Player playerScript = player.GetComponent<Player>();
        playerScript.SetupPlayer(index, choice.Name, playerInitialMoney, choice.Color, choice.AI, tiles);

        return playerScript;
    }

    private List<PlayerChoice> PlayersMock() {
        List<PlayerChoice> choices = new();
        PlayerChoice choice1 = new();
        choice1.Name = "test";
        choice1.Color = PlayerColor.BLUE;
        choice1.Initiative = Random.Range(1, 6);
        choice1.AI = true;

        choices.Add(choice1);

        PlayerChoice choice2 = new();
        choice2.Name = "test 2";
        choice2.Color = PlayerColor.YELLOW;
        choice2.Initiative = Random.Range(1, 6);
        choice2.AI = true;

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
        if (!player.IsMoving && !player.AI && !dice.GetEnabled()) {
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
        Player curPlayer = GetCurrentPlayer();
        curPlayer.StopRound();

        NextPlayer();
        StartRound();
    }

    private void StartRound() {
        positionIndicators.HighlightIndicator(curPlayerIndex);

        Player curPlayer = GetCurrentPlayer();
        dice.SetEnabled(true);
        dice.Clickable = !curPlayer.AI;
        curPlayer.StartRound();
    }

    public void DiceRollCallback(int result) {
        Player player = GetCurrentPlayer();
        player.MovePosition(result);
    }
}
