using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] float loadDelay;
    [SerializeField] AudioClip victorySound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem victoryParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    Rigidbody rBody;
    int currentSceneIndex;
    int nextSceneIndex;

    bool isTransitioning;
    bool debugMode;

    void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
        debugMode = false;
    }

    void Update() {
        DebugCollision();
    }

    void DebugCollision() {
        if (Input.GetKeyDown(KeyCode.C)) {
            DebugMode();
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
            Debug.Log("Debug Mode Activated: Level Skipped");
        }
    }

    void DebugMode() {
        debugMode = !debugMode;
        Debug.Log("Debug Mode = " + debugMode);
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || debugMode) {return;}
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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySound);
        victoryParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadDelay);
    }

    void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        crashParticle.Play();
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
