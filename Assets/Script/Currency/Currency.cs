using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currencyText;

    // Start is called before the first frame update
    void Start() {
        if (currencyText == null) {
            currencyText = gameObject.transform.Find("Currency").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateCurrency(int value) {
        currencyText.text = Utils.FormatPrice(value);
    }
}
