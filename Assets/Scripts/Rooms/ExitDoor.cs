using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExitDoor : MonoBehaviour
{
    public RoomController controller;

    void Reset() => GetComponent<Collider2D>().isTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (!controller) return;
        if (!other.CompareTag("Player")) return;
        controller.GoNextRoom();
    }
}
