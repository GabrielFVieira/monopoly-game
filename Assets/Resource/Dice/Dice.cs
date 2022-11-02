using System.Collections;
using UnityEngine;

public class Dice : MonoBehaviour {

    // Array of dice sides sprites to load from Resources folder
    public Sprite[] diceSides;

    // Reference to sprite renderer to change sprites
    public SpriteRenderer Dice1Sprite;
    public SpriteRenderer Dice2Sprite;

    // Use this for initialization
    private void Start () {

        // Assign Renderer component
        //rend = GetComponent<SpriteRenderer>();


	}
	
    // If you left click over the dice then RollTheDice coroutine is started
    private void OnMouseDown()
    {
        StartCoroutine("RollTheDice");
    }

    // Coroutine that rolls the dice
    private IEnumerator RollTheDice()
    {
        // Variable to contain random dice side number.
        // It needs to be assigned. Let it be 0 initially
        int randomDiceSide = 0;
        int randomDiceSide2 = 0;

        // Final side or value that dice reads in the end of coroutine
        int finalSide = 0;
        int finalSide2 = 0;

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
        finalSide = randomDiceSide + 1;
        finalSide2 = randomDiceSide2 + 1;

        // Show final dice value in Console
        Debug.Log(finalSide + finalSide2);
        GetComponent<PlayerMovement>().StartMoveJogador(finalSide + finalSide2);
    }
}
