using UnityEngine;

public class TankControls : MonoBehaviour {
    private Energy energy;
    private float boostEnergyPerSecond = 20f;
    private float normalSpeed = 10f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 20f;
    
    private void Awake() {
        energy = PlayerManager.Instance.energy;
    }
  
    private void Update() {
        if (!GameManager.Instance.gameActive) return;

        float energyUsed = boostEnergyPerSecond * Time.deltaTime;

        if (tag == "Player") {
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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.05f) {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
                }
            }
        }
    }
}