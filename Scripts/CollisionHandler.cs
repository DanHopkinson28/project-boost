using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Launch Pad":
                Debug.Log("Ready to Launch");
                break;
            case "Landing Pad":
                Debug.Log("Level complete");
                break;
            case "Fuel Pickup":
                Debug.Log("Refuelled");
                break;
            default:
                Debug.Log("You crashed");
                break;
        }
    }
}
