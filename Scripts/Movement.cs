using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainThrusters;
    [SerializeField] ParticleSystem leftSideThruster;
    [SerializeField] ParticleSystem rightSideThruster;
    
    AudioSource audioSource;
    Rigidbody rBody;

    float mainThrust = 1500f;
    float rotationThrust = 200f;

    void Start() {
        rBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        mainThrust = 1500f;
        rotationThrust = 200f;
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.Space)) {
            applyThrust();
        }
        else {
            killThrust();
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            RotateRight();
        }
        else {
            StopRotation();
        }
    }

    void RotateLeft() {
        ApplyRotation(rotationThrust);
        if(!rightSideThruster.isPlaying) {
            rightSideThruster.Play();
        }
    }

    void RotateRight() {
        ApplyRotation(-rotationThrust);
        if(!leftSideThruster.isPlaying) {
            leftSideThruster.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame) {
        rBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }

    void StopRotation() {
        leftSideThruster.Stop();
        rightSideThruster.Stop();
    }

    void applyThrust() {
        rBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if(!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        if(!mainThrusters.isPlaying) {
            mainThrusters.Play();
        }
    }

    void killThrust() {
        audioSource.Stop();
        mainThrusters.Stop();
    }
}
