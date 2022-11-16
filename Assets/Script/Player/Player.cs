using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField]
    public int Id { get; private set; }

    [field: SerializeField]
    public string Name { get; private set; }

    [field: SerializeField]
    public int Position { get; private set; }

    [field: SerializeField]
    public PlayerColor Color { get; private set; }

    [field: SerializeField]
    public Sprite Icon { get; private set; }

    [field: SerializeField]
    public int Money { get; set; }

    [SerializeField]
    private Tile[] ownedPropreties;

    [SerializeField]
    private PlayerColorSprite[] spriteColors;

    private BaseTile[] tiles;

    [field: SerializeField]
    public bool IsMoving { get; private set; }

    public void SetupPlayer(int id, string name, int money, PlayerColor color, BaseTile[] tiles) {
        Id = id;
        Name = name;
        Money = money;
        Color = color;

        foreach (PlayerColorSprite cs in spriteColors) {
            if (Color == cs.color) {
                GetComponent<SpriteRenderer>().sprite = cs.sprite;
                Icon = cs.icon;
                break;
            }
        }

        this.tiles = tiles;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePosition(int tileNum) {
        StartCoroutine(Move(tileNum));
    }

    public IEnumerator Move(int tileNum) {
        IsMoving = true;

        for (int i = 1; i <= tileNum; i++) {
            if (Position >= tiles.Length - 1) {
                Position = 0;
                transform.position = tiles[0].GetWaypoint().position;
            } else {
                transform.position = tiles[Position + 1].GetWaypoint().position;
                Position++;
            }

            yield return new WaitForSeconds(0.5f);
        }

        IsMoving = false;
    }
}
