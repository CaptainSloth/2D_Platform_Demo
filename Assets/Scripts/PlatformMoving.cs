using System.Collections;
using UnityEngine;

public class PlatformMoving : MonoBehaviour {

    private Vector3 startLoc;
    private Vector3 endLoc;
    private Vector3 nextLoc;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform movingPlatform;

    [SerializeField]
    private Transform endLocation;

    void Start()
    {
        startLoc = movingPlatform.localPosition;
        endLoc = endLocation.localPosition;

        nextLoc = endLoc;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        movingPlatform.localPosition = Vector3.MoveTowards(movingPlatform.localPosition, nextLoc, speed * Time.deltaTime);

        if(Vector3.Distance(movingPlatform.localPosition,nextLoc) <= 0.1)
        {
            ChangeDest();
        }
    }

    private void ChangeDest()
    {
        nextLoc = nextLoc != startLoc ? startLoc : endLoc;
    }
}
