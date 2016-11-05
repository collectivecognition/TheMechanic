using UnityEngine;
using System.Collections;

public class ParkingPoint : MonoBehaviour {
    private bool entered = false;
    private GameObject humanPlayer;

    void Update() {
        float distance = Vector3.Distance(GameManager.Instance.player.transform.position, transform.position);

        if (entered) {
            if (distance < 1f) {
                CameraManager.Instance.ZoomIn(false, () => {
                    AnimatePlayer();
                });
                entered = false;
            } else {
                GameManager.Instance.player.transform.position = Vector3.Lerp(GameManager.Instance.player.transform.position, transform.position, 5f * Time.deltaTime);
                GameManager.Instance.player.transform.rotation = Quaternion.Lerp(GameManager.Instance.player.transform.rotation, transform.rotation, 5f * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (Is.APlayerTank(collider.gameObject)) {
            collider.gameObject.GetComponent<TankControls>().controlsEnabled = false;
            entered = true;
        }
    }

    private void AnimatePlayer() {
        GameObject playerPrefab = (GameObject)Resources.Load("Prefabs/PlayerHuman");
        humanPlayer = GameObject.Instantiate(playerPrefab);

        humanPlayer.transform.position = iTweenPath.GetPath("ParkingPath")[0];
        humanPlayer.GetComponent<HumanControls>().Walk();
        humanPlayer.GetComponent<HumanControls>().controlsEnabled = false;

        iTween.MoveTo(humanPlayer, iTween.Hash(
            "path", iTweenPath.GetPath("ParkingPath"),
            "orienttopath", true,
            "lookahead", 0.5f,
            "axis", "y",
            "time", 2f, 
            "loopType", iTween.LoopType.none, 
            "easeType", iTween.EaseType.linear, 
            "onComplete", "OpenDoor",
            "onCompleteTarget", gameObject
        ));
    }

    private void OpenDoor() {
        humanPlayer.GetComponent<HumanControls>().Stop();
        GameObject.Find("BuildingWallWithDoor/Door").GetComponent<DoorPressurePlate>().Open();
        // TODO: Destroy humanPlayer?
    }
}
