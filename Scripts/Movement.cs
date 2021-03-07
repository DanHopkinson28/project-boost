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
            rBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying) {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!mainThrusters.isPlaying) {
                mainThrusters.Play();
            }
        }
        else {
            audioSource.Stop();
            mainThrusters.Stop();
        }
    }
    void ProcessRotation() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            ApplyRotation(rotationThrust);
            if(!rightSideThruster.isPlaying) {
                rightSideThruster.Play();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            ApplyRotation(-rotationThrust);
            if(!leftSideThruster.isPlaying) {
                leftSideThruster.Play();
            }
        }
        else {
            leftSideThruster.Stop();
            rightSideThruster.Stop();
        }
    }
    void ApplyRotation(float rotationThisFrame) {
        rBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
