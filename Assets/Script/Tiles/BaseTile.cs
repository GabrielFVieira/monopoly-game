using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

abstract public class BaseTile: MonoBehaviour {
    [SerializeField]
    protected int id;

    [SerializeField]
    protected Transform waypoint;

    [SerializeField]
    protected GameObject highlight;

    protected int initialSortingOrder;

    protected void Start() {
        if (waypoint == null) {
            waypoint = gameObject.transform.Find("Waypoint");
        }

        if (highlight == null) {
            highlight = gameObject.transform.Find("Highlight").gameObject;
        }

        initialSortingOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    public Transform GetWaypoint() {
        return waypoint;
    }

    public int GetId() {
        return id;
    }

    public void SetId(int newId) {
        id = newId;
    }

    protected void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        highlight.SetActive(true);
        GetComponent<SortingGroup>().sortingOrder = initialSortingOrder + 5;
    }

    protected void OnMouseExit() {
        highlight.SetActive(false);
        GetComponent<SortingGroup>().sortingOrder = initialSortingOrder;
    }

    public abstract void ExecuteAction(Player player);
}
