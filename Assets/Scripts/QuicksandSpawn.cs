using System.Collections;
using UnityEngine;

public class QuicksandSpawn : MonoBehaviour {

    public GameObject quicksandGFX;
    public Transform spawnPoint;

    private GameObject quicksandInstance;

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "QuicksandGFX")
        {
            Debug.Log("fadsf");
            GFXspawn();
        }
        
    }


    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.collider.CompareTag("QuicksandGFX"))
    //    {
    //        Debug.Log("fadsf");
    //        GFXspawn();
    //    }
    //}

    void GFXspawn()
    {
        var quicksandInstance = Instantiate(quicksandGFX, spawnPoint.position, spawnPoint.rotation) as GameObject;
    }
}
