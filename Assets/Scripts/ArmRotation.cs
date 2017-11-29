using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {

    public int rotationOffset = 90;

    void Update () {
        Vector3 mouseDiff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDiff.Normalize();

        float rotZ = Mathf.Atan2(mouseDiff.y, mouseDiff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }
}
