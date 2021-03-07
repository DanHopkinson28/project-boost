using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    int currentSceneIndex;
    int nextSceneIndex;
    [SerializeField] float loadDelay;

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
                StartSuccessSequence();
                break;
            case "Fuel Pickup":
                Debug.Log("Refuelled");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence() {
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }

    void StartCrashSequence() {
        // Todo: Add SFX upon crash
        // Todo: Add Particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
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
