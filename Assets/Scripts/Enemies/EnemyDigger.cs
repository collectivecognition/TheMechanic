using UnityEngine;
using System.Collections;

public class EnemyDigger : Enemy {
    void Start() {
        base.Start();

        transform.position += Vector3.down * 5f;

        PopUp();
    }

    void Update() {
        if (!GameManager.Instance.gameActive) return;

    }

    void PopUp() {
        iTween.MoveAdd(gameObject, iTween.Hash("y", 5f, "time", 2f));
    }

    void PopDown() {
        iTween.MoveAdd(gameObject, iTween.Hash("y", -5f, "time", 2f));
    }
}
