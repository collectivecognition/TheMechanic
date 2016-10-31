using UnityEngine;

public class TankControls : MonoBehaviour {
    private Energy energy;
    private SpawnPoint humanSpawnPoint;
    private Transform bodyTransform;
    private Transform frontLeftTireTransform;
    private Transform frontRightTireTransform;
    private Transform backLeftTireTransform;
    private Transform backRightTireTransform;

    private float boostEnergyPerSecond = 20f;
    private float normalSpeed = 30f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 50f;
    public bool controlsEnabled = true;
    
    private void Awake() {
        energy = PlayerManager.Instance.energy;
        humanSpawnPoint = transform.Find("HumanSpawnPoint").GetComponent<SpawnPoint>();
        bodyTransform = transform.Find("TankBody");

        frontLeftTireTransform = transform.Find("TankBody/WheelFrontLeft");
        frontRightTireTransform = transform.Find("TankBody/WheelFrontRight");
        backLeftTireTransform = transform.Find("TankBody/WheelBackLeft");
        backRightTireTransform = transform.Find("TankBody/WheelBackRight");
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

            Vector3 xzDirection = new Vector3(x, 0, y);
            Quaternion targetRotation = Quaternion.LookRotation(xzDirection);
            //targetRotation *= Quaternion.Euler(0, -45, 0);
            bodyTransform.rotation = Quaternion.RotateTowards(bodyTransform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            float angle = Quaternion.Angle(bodyTransform.rotation, targetRotation);

            if (angle < 1f) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + bodyTransform.forward, speed * Time.deltaTime);

                frontLeftTireTransform.localEulerAngles = new Vector3(frontLeftTireTransform.localEulerAngles.x, 0f, frontLeftTireTransform.localEulerAngles.z);
                frontRightTireTransform.localEulerAngles = new Vector3(frontRightTireTransform.localEulerAngles.x, 0f, frontRightTireTransform.localEulerAngles.z);
                backLeftTireTransform.localEulerAngles = new Vector3(backLeftTireTransform.localEulerAngles.x, 0f, backLeftTireTransform.localEulerAngles.z);
                backRightTireTransform.localEulerAngles = new Vector3(backRightTireTransform.localEulerAngles.x, 0f, backRightTireTransform.localEulerAngles.z);
            } else {
                float turnAngle = bodyTransform.eulerAngles.y > targetRotation.eulerAngles.y ? 45f : -45f;
                frontLeftTireTransform.localEulerAngles = new Vector3(frontLeftTireTransform.localEulerAngles.x, turnAngle, frontLeftTireTransform.localEulerAngles.z);
                frontRightTireTransform.localEulerAngles = new Vector3(frontRightTireTransform.localEulerAngles.x, turnAngle, frontRightTireTransform.localEulerAngles.z);
                backLeftTireTransform.localEulerAngles = new Vector3(backLeftTireTransform.localEulerAngles.x, turnAngle, backLeftTireTransform.localEulerAngles.z);
                backRightTireTransform.localEulerAngles = new Vector3(backRightTireTransform.localEulerAngles.x, turnAngle, backRightTireTransform.localEulerAngles.z);
            }

            frontLeftTireTransform.Rotate(Vector3.right * Time.deltaTime * 360f);
            frontRightTireTransform.Rotate(Vector3.right * Time.deltaTime * 360f);
            backLeftTireTransform.Rotate(Vector3.right * Time.deltaTime * 360f);
            backRightTireTransform.Rotate(Vector3.right * Time.deltaTime * 360f);
        }
    }
}