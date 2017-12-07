using System.Collections;
using UnityEngine;

public class QuicksandTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "QuicksandParent")
            return; // do nothing
        Debug.Log("child goes ouch!");
    }

}
