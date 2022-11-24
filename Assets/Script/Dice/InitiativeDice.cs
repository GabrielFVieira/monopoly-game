using UnityEngine;
using UnityEngine.UI;

public class InitiativeDice : Dice {
    [SerializeField] private PlayerSetup playerSetup;

    // Reference to image to change sprites
    [SerializeField] private Image dice1Image;
    [SerializeField] private Image dice2Image;

    void Start() {
        //if (gameManager == null) {
        //    gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //}
    }

    protected override void Action(int result) {
        playerSetup.SetPlayerInitiative(result);
    }

    protected override void UpdateDice1Image(Sprite sprite) {
        dice1Image.sprite = sprite;
    }

    protected override void UpdateDice2Image(Sprite sprite) {
        dice2Image.sprite = sprite;
    }

    protected override void UpdateDice1Color(Color color) {
        dice1Image.color = color;
    }

    protected override void UpdateDice2Color(Color color) {
        dice2Image.color = color;
    }
}
