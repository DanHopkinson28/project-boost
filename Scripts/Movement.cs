﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float mainThrust = 1500f;
    float rotationThrust = 200f;
    Rigidbody rBody;

    void Start() {
        rBody = GetComponent<Rigidbody>();
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
        }
    }
    void ProcessRotation() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            ApplyRotation(-rotationThrust);
        }
    }
    void ApplyRotation(float rotationThisFrame) {
        rBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
