using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class TileColorSprite
{
    public TileColor tileColor;
    public Sprite tileSprite;
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private string title;

    [SerializeField]
    private TileColor color;

    [SerializeField]
    private int price;

    [SerializeField]
    private TileStatus status;

    [SerializeField]
    private TileDetails details;

    [SerializeField]
    private TextMeshPro nameObj;

    [SerializeField]
    private TextMeshPro priceObj;

    [SerializeField]
    private GameObject houseCountObj;

    [SerializeField]
    private TextMeshPro houseCountTextObj;

    [SerializeField]
    private GameObject hotelObj;

    [SerializeField]
    private TileColorSprite[] spriteColors;

    [SerializeField]
    private GameObject ownerIcon;

    [SerializeField]
    private Player Owner;

    // Start is called before the first frame update
    void Start() {
        status = TileStatus.NOT_BOUGHT;

        if (nameObj == null) {
            nameObj = gameObject.transform.Find("Name").GetComponent<TextMeshPro>();
        }

        if (priceObj == null) {
            priceObj = gameObject.transform.Find("Price").GetComponent<TextMeshPro>();
        }

        if (ownerIcon == null) {
            ownerIcon = gameObject.transform.Find("Owner").gameObject;
        }


        ownerIcon.SetActive(false);
        nameObj.text = title;
        priceObj.text = FormatPrice(price);

        foreach (TileColorSprite cs in spriteColors) {
            if (color == cs.tileColor) {
                GetComponent<SpriteRenderer>().sprite = cs.tileSprite;
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }

    public TileStatus GetStatus() {
        return status;
    }

    public void SetTileStatus(TileStatus newStatus) {
        status = newStatus;

        if (status >= TileStatus.ONE_HOUSE && status <= TileStatus.FOUR_HOUSES) {
            int amount = (int)status - 1;
            houseCountTextObj.text = "x" + amount.ToString();
            houseCountObj.SetActive(true);
            hotelObj.SetActive(false);
        } else if (status == TileStatus.HOTEL) {
            hotelObj.SetActive(true);
            houseCountObj.SetActive(false);
        } else {
            hotelObj.SetActive(false);
            houseCountObj.SetActive(false);
        }
    }

    public void SetOwner(Player player) {
        if (player == null) {
            Owner = null;
            ownerIcon.SetActive(false);
        }

        Owner = player;
        ownerIcon.GetComponent<SpriteRenderer>().color = Owner.GetColor();
        ownerIcon.SetActive(true);
    }

    private void OnMouseDown() {
        Debug.Log("Tile mouse click");    
    }

    private void OnMouseOver()
    {
        Debug.Log("Tile mouse over");
    }

    private string FormatPrice(int price) {      
        return "$" + price.ToString("N0");
    }

    public void ShowDetails() {

    }
}
