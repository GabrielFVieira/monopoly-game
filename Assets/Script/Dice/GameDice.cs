using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameDice : Dice
{
    [SerializeField] private GameManager gameManager;

    // Reference to sprite renderer to change sprites
    [SerializeField] private SpriteRenderer dice1Sprite;
    [SerializeField] private SpriteRenderer dice2Sprite;

    void Start()
    {
        if (gameManager == null) {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
    }

    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseUp() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Roll();
    }

    protected override void Action(int result) {
        gameManager.DiceRollCallback(result);
    }

    protected override void UpdateDice1Image(Sprite sprite) {
        dice1Sprite.sprite = sprite;
    }

    protected override void UpdateDice2Image(Sprite sprite) {
        dice2Sprite.sprite = sprite;
    }

    protected override void UpdateDice1Color(Color color) {
        dice1Sprite.color = color;
    }

    protected override void UpdateDice2Color(Color color) {
        dice2Sprite.color = color;
    }
}
