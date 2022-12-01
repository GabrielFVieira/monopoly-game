using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField]
    public int Id { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int Position { get; private set; }

    [field: SerializeField]
    public PlayerColor Color { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public int Money { get; set; }

    [SerializeField]
    private Tile[] ownedPropreties;

    [SerializeField]
    private PlayerColorSprite[] spriteColors;

    private BaseTile[] tiles;

    [field: SerializeField]
    public bool IsMoving { get; private set; }

    [SerializeField]
    private int initialSortOrder = 3;

    [field: SerializeField]
    public bool AI { get; private set; }

    [SerializeField]
    private GameDice dice;

    [SerializeField]
    private GameManager gameManager;

    public void SetupPlayer(int id, string name, int money, PlayerColor color, bool ai, BaseTile[] tiles) {
        Id = id;
        Name = name;
        Money = money;
        Color = color;
        AI = ai;

        foreach (PlayerColorSprite cs in spriteColors) {
            if (Color == cs.color) {
                GetComponent<SpriteRenderer>().sprite = cs.sprite;
                Icon = cs.icon;
                break;
            }
        }

        this.tiles = tiles;        
    }

    public void Awake() {
        dice = GameObject.FindGameObjectWithTag("GameDice").GetComponent<GameDice>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        initialSortOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePosition(int tileNum) {
        StartCoroutine(Move(tileNum));
    }

    public IEnumerator Move(int tileNum) {
        IsMoving = true;

        for (int i = 1; i <= tileNum; i++) {
            if (Position >= tiles.Length - 1) {
                Position = 0;
                transform.position = tiles[0].GetWaypoint().position;

                if (i != tileNum) {
                    // Tiles 0 is the Go tile, when passing throw it you should call it's action
                    tiles[0].ExecuteAction(this);
                }

            } else {
                transform.position = tiles[Position + 1].GetWaypoint().position;
                Position++;
            }

            yield return new WaitForSeconds(0.5f);
        }

        IsMoving = false;
        PostMove();
    }

    private void StartHighlight() {
        GetComponent<SpriteRenderer>().sortingOrder = initialSortOrder + 1;
    }

    private void StopHighlight() {
        GetComponent<SpriteRenderer>().sortingOrder = initialSortOrder;
    }

    public void StartRound() {
        StartHighlight();

        if (AI) {
            dice.Roll();
        }
    }

    private void PostMove() {
        tiles[Position].ExecuteAction(this);

        if (!AI) {
            return;
        }



        gameManager.PassTurn();
    }

    public void StopRound() {
        StopHighlight();
    }

    public void BuyPlace() {
    }

    public void Buy() {
    }

    public void Sell() {
    }

    // add money to player
    public void Receive(int amount) {
        Money += amount;
        gameManager.UpdateCurrencyPanel(this);
    }

    // remove money from the player
    public void Pay(int amount) {
        Money -= amount;
        gameManager.UpdateCurrencyPanel(this);
    }

    public void Die() {
    }
}
