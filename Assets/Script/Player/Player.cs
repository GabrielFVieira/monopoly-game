using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    [SerializeField]
    private int Id;

    public string Name { get; set; }

    public string Piece { get; set; }

    public List<Card> Cards { get; set; }

    public int Position { get; set; }

    public int Initiative { get; set; }

    public bool Ready { get; set; }

    [SerializeField]
    private Color color;

    [SerializeField]
    public long Money;

    [SerializeField]
    private Tile[] ownedPropreties;

    public Color GetColor() {
        return color;
    }

    public void SetColor(Color c) {
        color = c;
    }

    public Sprite GetImage() {
        return GetComponent<SpriteRenderer>().sprite;
    }

    // public void RollDiceMovement() {
    //     bool equal = true;
    //     int rollTimes = 1;
    //     int movement = 0;
    //     int dice1 = 0;
    //     int dice2 = 0;
    //     while(equal) {
    //         // roll both dices
    //         if(rollTimes >= 3) {
    //             // go to jail
    //             this.Position = 10; // position to go to jail
    //             equal = false;
    //             return;
    //         }
    //         dice1 = Random.Range(1, 7);
    //         dice2 = Random.Range(1, 7);
    //         equal = dice1 == dice2;
    //         rollTimes++;
    //     }
    //     movement = dice1 + dice2;
    //     Move(movement);
    // }

    public void Move(int movement) {
        // move player
    }

    public void BuyPlace() {

    }

    public void Buy() {

    }

    public void Sell() {

    }

    public int GetId() {
        return Id;
    }

    public void SetId(int id) {
        this.Id = id;
    }

    public void Pay(int amount, Player? player) {
        if (this.Money >= amount) {
            this.Money -= amount;
            if(player != null) {
                player.Receive(amount);
            } else {
                // Bank
            }
        } else {
            // Player is bankrupt, e.g: died, init game options to recover or game over
            // TODO: init game options to recover, for now, the player just dies
            if(this.Money > 0 && player != null) {
                // if dying and was paying to another player
                // give all remaining money to the other player
                player.Receive(this.Money);
            }
            this.Money = 0;
            Die();
        }
    }

    // add money to player
    public void Receive(long amount)
    {
        this.Money += amount;
    }

    public void Die() {
        
    }

}
