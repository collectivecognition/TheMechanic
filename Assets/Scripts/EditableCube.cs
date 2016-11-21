using UnityEngine;
using System.Collections;

public class EditableCube : MonoBehaviour {
    public void Snap() {
        transform.position = new Vector3(Mathf.Round(transform.position.x / Grid.size) * Grid.size + Grid.size / 2f, Mathf.Round(transform.position.y / Grid.size) * Grid.size + Grid.size / 2f, Mathf.Round(transform.position.z / Grid.size) * Grid.size + Grid.size / 2f);
    }

    public void Move(Vector3 direction) {
        transform.position += direction;
    }

    public void Duplicate(Vector3 direction) {
        GameObject.Instantiate(gameObject, gameObject.transform.parent);
        Move(direction);
    }
}
