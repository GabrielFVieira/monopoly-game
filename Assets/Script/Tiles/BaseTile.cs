using UnityEngine;

abstract public class BaseTile: MonoBehaviour {
    [SerializeField]
    protected int id;

    [SerializeField]
    protected Transform waypoint;

    protected void SetWaypoint() {
        waypoint = gameObject.transform.Find("Waypoint");
    }

    public Transform GetWaypoint() {
        return waypoint;
    }

    public void SetId(int newId) {
        id = newId;
    }

    public abstract void ExecuteAction(Player player);
}
