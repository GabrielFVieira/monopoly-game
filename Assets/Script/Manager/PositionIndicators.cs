using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionIndicators : MonoBehaviour
{
    [SerializeField] private GameObject redPlayerPositionIndicator;
    [SerializeField] private GameObject bluePlayerPositionIndicator;
    [SerializeField] private GameObject greenPlayerPositionIndicator;
    [SerializeField] private GameObject yellowPlayerPositionIndicator;
    [SerializeField] private GameObject currentPlayerPositionIndicator;

    [SerializeField] private float space = 0.75f;

    [SerializeField] private GameObject[] order;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupIndicators(Player[] playerOrder) {
        order = new GameObject[playerOrder.Length];

        for (int i = 0; i < playerOrder.Length; i++) {
            GameObject indicator = GetIndicator(playerOrder[i].Color);
            indicator.transform.position += new Vector3(space * i, 0, 0);
            indicator.transform.GetChild(0).gameObject.SetActive(playerOrder[i].AI);
            indicator.SetActive(true);

            order[i] = indicator;
        }
    }

    public void HighlightIndicator(int pos) {
        if (pos < 0) {
            pos = 0;
        } else if (pos >= order.Length) {
            pos = order.Length - 1;
        }

        GameObject indicator = order[pos];
        currentPlayerPositionIndicator.transform.position = indicator.transform.position;

        if (!currentPlayerPositionIndicator.activeSelf) {
            currentPlayerPositionIndicator.SetActive(true);
        }
    }

    private GameObject GetIndicator(PlayerColor color) {
        GameObject result;

        switch (color) {
            case PlayerColor.BLUE:
                result = bluePlayerPositionIndicator;
                break;
            case PlayerColor.GREEN:
                result = greenPlayerPositionIndicator;
                break;
            case PlayerColor.RED:
                result = redPlayerPositionIndicator;
                break;
            default:
                result = yellowPlayerPositionIndicator;
                break;
        }
        return result;
    }
}
