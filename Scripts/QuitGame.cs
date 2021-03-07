using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour {

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("The Application has quit");
            Quit();
        }
    }

    void Quit() {
        Application.Quit();
    }
}
