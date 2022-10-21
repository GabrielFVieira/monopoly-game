using UnityEngine;

public class Player
{
    [SerializeField]
    private Color color;

    public Color GetColor() {
        return color;
    }

    public void SetColor(Color c) {
        color = c;
    }
}
