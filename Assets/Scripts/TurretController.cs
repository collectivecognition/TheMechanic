using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
    private float speed = 50f;
    public GameObject turretObject;
	
	void Update () {
        turretObject.transform.Rotate(Vector3.forward *  Input.GetAxis("Right Stick Horizontal") * speed * Time.deltaTime);
    }
}
