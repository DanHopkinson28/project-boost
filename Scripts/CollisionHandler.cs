using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    int currentSceneIndex;
    int nextSceneIndex;

    void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
    }

    void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Launch Pad":
                Debug.Log("Ready to Launch");
                break;
            case "Landing Pad":
                LoadNextLevel();
                break;
            case "Fuel Pickup":
                Debug.Log("Refuelled");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel() {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel() {
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            RestartGame();    
        } else {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    void RestartGame() {
        SceneManager.LoadScene(0);   
    }
}
