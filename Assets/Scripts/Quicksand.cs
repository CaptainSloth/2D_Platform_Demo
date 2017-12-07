using System.Collections;
using UnityEngine;

public class Quicksand : MonoBehaviour {

    public bool InQuicksand = false;

    private Rigidbody2D rb;

    private Vector3 startLoc;
    private Vector3 endLoc;
    private Vector3 nextLoc;
    private Vector3 spawnLoc;
    private Vector3 triggerLoc;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform quicksandGFX;

    [SerializeField]
    private Transform endLocation;

    [SerializeField]
    private Transform spawnPointTrigger;

    [SerializeField]
    private Transform spawnPoint;

    void Start () 
	{
        rb = GetComponent<Rigidbody2D>();
        startLoc = quicksandGFX.localPosition;
        endLoc = endLocation.localPosition;
        nextLoc = endLoc;
        spawnLoc = spawnPoint.localPosition;
        triggerLoc = spawnPointTrigger.localPosition;
    }

    //void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        // Debug.Log("Quicksand Enter");
    //       // InQuicksand = true;
    //        quicksandGFX.localPosition = Vector3.MoveTowards(quicksandGFX.localPosition, nextLoc, speed * Time.deltaTime);
    //        if (Vector3.Distance(quicksandGFX.localPosition, nextLoc) <= 0.1)
    //        {
    //            Debug.Log("Quicksand End");
    //            // StartCoroutine(SpawnNewPlatform());
    //            DeletePlatform();
    //        }
    //    }
    //}

    void Update () 
	{
       // GameObject clonePed = Instantiate(Ped, new Vector3(0, 0, -2), Quaternion.identity) as GameObject;
        quicksandGFX.localPosition = Vector3.MoveTowards(quicksandGFX.localPosition, nextLoc, speed * Time.deltaTime);
        if (Vector3.Distance(quicksandGFX.localPosition, triggerLoc) <= 0.1)
        {
            SpawnNewPlatform();
        }
        if (Vector3.Distance(quicksandGFX.localPosition, nextLoc) <= 0.1)
        {
            DeletePlatform();
        }
    }

    void SpawnNewPlatform()
    {
        Instantiate(quicksandGFX, spawnLoc, Quaternion.identity);
    }

    private void DeletePlatform()
    {
        Destroy(quicksandGFX);
    }

    // Instead of forcing the gameobject to be triggered by player, have the GO move X then trigger quicksandPrefab.tag to spawn new quicksandPrefab. 
}
