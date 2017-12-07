using System.Collections;
using UnityEngine;

public class QuicksandMovement : MonoBehaviour {


    private Vector3 startLoc;
    private Vector3 endLoc;
    private Vector3 nextLoc;

    [SerializeField]
    private Transform movingPlatform;

    public float speed = 1f;

    [SerializeField]
    private Transform endLocation;

    void Start()
    {
        startLoc = movingPlatform.localPosition;
        endLoc = endLocation.localPosition;

//        nextLoc = endLoc;

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        movingPlatform.localPosition = Vector3.MoveTowards(movingPlatform.localPosition, endLoc, speed * Time.deltaTime);

        if (Vector3.Distance(movingPlatform.localPosition, endLoc) <= 0.1)
        {
            Destroy(gameObject);
        }
    }



}
