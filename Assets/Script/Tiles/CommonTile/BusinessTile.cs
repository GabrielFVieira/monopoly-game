using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BusinessTile : BaseTile
{
    [field: SerializeField]
    public string Title { get; private set; }

    [field: SerializeField]
    public int Price { get; private set; }

    [field: SerializeField]
    public TileStatus Status { get; private set; }

    [field: SerializeField]
    public int ValueMultiplier { get; private set; } = 500;

    [field: SerializeField]
    public Player Owner { get; private set; }

    [SerializeField]
    private GameObject ownerIcon;

    [SerializeField]
    private TextMeshPro nameObj;

    [SerializeField]
    private TextMeshPro priceObj;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private BusinessDetailController detailController;

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        FillControlObjects();

        initialSortingOrder = GetComponent<SortingGroup>().sortingOrder;

        Status = TileStatus.NOT_BOUGHT;
        ownerIcon.SetActive(false);
        nameObj.text = Title;
        priceObj.text = Utils.FormatPrice(Price);
        highlight.SetActive(false);
    }

    private void FillControlObjects() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        if (nameObj == null) {
            nameObj = gameObject.transform.Find("Name").GetComponent<TextMeshPro>();
        }

        if (priceObj == null) {
            priceObj = gameObject.transform.Find("Price").GetComponent<TextMeshPro>();
        }

        if (ownerIcon == null) {
            ownerIcon = gameObject.transform.Find("Owner").gameObject;
        }

        if (detailController == null) {
            detailController = GameObject.FindGameObjectWithTag("BusinessTileDetail").GetComponent<BusinessDetailController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        Player curPlayer = gameManager.GetCurrentPlayer();

        if (curPlayer != null) {
            ShowDetails(curPlayer);
        }
    }

    public override void ExecuteAction(Player player) {
        ShowDetails(player);
    }

    private void ShowDetails(Player player) {
        detailController.ShowDetails(this, player);
    }

    private void UpdateOwner(Player player) {
        if (player == null) {
            Owner = null;
            ownerIcon.SetActive(false);

            return;
        }

        Owner = player;
        ownerIcon.GetComponent<SpriteRenderer>().color = player.GetColor();
        ownerIcon.SetActive(true);
    }

    public void BuyProperty(Player player) {
        if (Status != TileStatus.NOT_BOUGHT) {
            // Throw error
            return;
        }

        Status = TileStatus.PURCHASED;
        UpdateOwner(player);
    }

    public void SellProperty() {
        Status = TileStatus.NOT_BOUGHT;
        UpdateOwner(null);
    }
}
