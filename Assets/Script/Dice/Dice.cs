using System.Collections;
using UnityEngine;

public abstract class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    [SerializeField] protected Sprite[] diceSides;

    [SerializeField] protected bool enableRoll;
    [SerializeField] protected bool running;

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
            UpdateDice1Color(Color.gray);
            UpdateDice2Color(Color.gray);
        } else {
            UpdateDice1Color(Color.white);
            UpdateDice2Color(Color.white);
        }

        enableRoll = value;
    }

    // Coroutine that rolls the dice
    protected IEnumerator RollTheDice()
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
            UpdateDice1Image(diceSides[randomDiceSide]);
            UpdateDice2Image(diceSides[randomDiceSide2]);

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

        Action(result);

        running = false;
        SetEnabled(false);
    }

    protected abstract void Action(int result);
    protected abstract void UpdateDice1Image(Sprite sprite);
    protected abstract void UpdateDice2Image(Sprite sprite);
    protected abstract void UpdateDice1Color(Color color);
    protected abstract void UpdateDice2Color(Color color);
}

