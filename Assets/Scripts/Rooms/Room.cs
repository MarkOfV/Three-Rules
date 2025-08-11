using UnityEngine;

namespace Rooms
{
    public enum RoomKind { Combat, Vendor, Boss }
    public class Room : MonoBehaviour
    {
        public Transform playerSpawn;
        public Transform[] enemySpawns;
        public GameObject hazardsParent;         // can be null
        public Collider2D exitTrigger;
        public RoomKind kind;          // optional; disabled at start
    }
}