using UnityEngine;
using System.Collections;

public class TankDistanceCounter : MonoBehaviour {
    public float totalDistance {
        get {
            return _totalDistance;
        }    
    }

    private float _totalDistance = 0;
    private Vector3 lastPosition;

    void Start () {
        Reset();
    }

	void Update () {
        Vector3 currentPosition = transform.position;
        currentPosition.y = 0f;

        float distance = Vector3.Distance(currentPosition, lastPosition);
        _totalDistance += distance;

        lastPosition = currentPosition;
	}

    public void Reset () {
        _totalDistance = 0;
        lastPosition = transform.position;
        lastPosition.y = 0f;
    }
}
