using UnityEngine;

public class TankControls : MonoBehaviour {
    private Energy energy;
    private SpawnPoint humanSpawnPoint;
    private Transform bodyTransform;

    private float boostEnergyPerSecond = 20f;
    private float normalSpeed = 30f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 50f;
    public bool controlsEnabled = true;
    
    private void Awake() {
        energy = PlayerManager.Instance.energy;
        humanSpawnPoint = transform.Find("HumanSpawnPoint").GetComponent<SpawnPoint>();
        bodyTransform = transform.Find("TankBody");
    }

    private void GetOut() {
        humanSpawnPoint.Spawn();

        controlsEnabled = false;
        GetComponent<Interactable>().enabled = true;
    }

    public void GetIn() {
        controlsEnabled = true;
        GetComponent<Interactable>().enabled = true;
        Destroy(GameManager.Instance.player);
        GameManager.Instance.player = gameObject;
        CameraManager.Instance.ZoomOut();
    }

    public void OnInteraction() {
        GetIn();
    }
  
    private void Update() {
        if (!GameManager.Instance.gameActive || !controlsEnabled) return;

        if (Input.GetKeyDown(KeyCode.O)) {
            if (!BattleManager.Instance.BattleActive) {
                GetOut();
            }
        }

        float energyUsed = boostEnergyPerSecond * Time.deltaTime;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0) {
            float speed = normalSpeed;

            if (Input.GetKey(KeyCode.LeftShift) && energy.current >= energyUsed) {
                energy.Use(energyUsed);

                if (energy.current > 1f) { // We need a small buffer to prevent stuttering
                    speed = boostSpeed;
                }
            }

            Vector3 xzDirection = new Vector3(y, 0, -x);
            Quaternion targetRotation = Quaternion.LookRotation(xzDirection);
            targetRotation *= Quaternion.Euler(0, -45, 0);
            bodyTransform.rotation = Quaternion.RotateTowards(bodyTransform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (Quaternion.Angle(bodyTransform.rotation, targetRotation) < 1f) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + bodyTransform.forward, speed * Time.deltaTime);
            }
        }
    }
}