using UnityEngine;

public class TankControls : MonoBehaviour {
    private TankEnergy energy;
    private float boostEnergyPerSecond = 20f;
    private float normalSpeed = 10f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 20f;
    
    private void Awake() {
        energy = GetComponent<TankEnergy>();
    }
  
    private void Update() {
        float energyUsed = boostEnergyPerSecond * Time.deltaTime;

        if (GameManager.Instance.gameActive && tag == "Player") {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0) {
                float speed = normalSpeed;

                if (Input.GetKey(KeyCode.LeftShift) && energy.Energy >= energyUsed) {
                    energy.UseEnergy(energyUsed);

                    if(energy.Energy > 1f) { // We need a small buffer to prevent stuttering
                        speed = boostSpeed;
                    }
                }

                Vector3 xzDirection = new Vector3(y, 0, -x);
                Quaternion targetRotation = Quaternion.LookRotation(xzDirection);
                targetRotation *= Quaternion.Euler(0, -45, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
            }
        }
    }
}