using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    [SerializeField] private Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    [SerializeField] private SpriteRenderer Dice1Sprite;
    [SerializeField] private SpriteRenderer Dice2Sprite;

    [SerializeField] private bool enableRoll;
    [SerializeField] private bool running;

    [SerializeField] private GameManager gameManager;

    private void Start() {
        if (gameManager == null) {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }
    }

    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Roll();
    }

    public void Roll() {
        if (enableRoll && !running) {
            running = true;
            StartCoroutine(RollTheDice());
        }
    }

    public bool GetEnabled() {
        return enableRoll;
    }

    public void SetEnabled(bool value) {
        if (!value) {
            Dice1Sprite.color = Color.gray;
            Dice2Sprite.color = Color.gray;
        } else {
            Dice1Sprite.color = Color.white;
            Dice2Sprite.color = Color.white;
        }

        enableRoll = value;
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;
        int randomDiceSide2 = 0;

        // Loop to switch dice sides ramdomly
        // before final side appears. 20 itterations here.
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            randomDiceSide = Random.Range(0, 5);
            randomDiceSide2 = Random.Range(0, 5);

            // Set sprite to upper face of dice from array according to random value
            Dice1Sprite.sprite = diceSides[randomDiceSide];
            Dice2Sprite.sprite = diceSides[randomDiceSide2];

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }

        // Assigning final side so you can use this value later in your game
        // for player movement for example
        int finalSide = randomDiceSide + 1;
        int finalSide2 = randomDiceSide2 + 1;

        int result = finalSide + finalSide2;

        // Show final dice value in Console
        Debug.Log("Dice result: " + result);

        yield return new WaitForSeconds(0.3f);

        gameManager.DiceRollCallback(result);

        running = false;
        SetEnabled(false);
    }
}
