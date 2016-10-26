using UnityEngine;

public class HumanControls : MonoBehaviour {
    private float normalSpeed = 10f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 20f;

    private Animator animator;

    private void Awake() {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    private void Update() {
        if (!GameManager.Instance.gameActive) {
            animator.SetBool(Animator.StringToHash("isWalking"), false);
            animator.SetBool(Animator.StringToHash("isRunning"), false);

            return;
        }
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0) {
            float speed = normalSpeed;

            animator.SetBool(Animator.StringToHash("isWalking"), true);
            animator.SetBool(Animator.StringToHash("isRunning"), false);

            if (Input.GetKey(KeyCode.LeftShift)) {
                speed = boostSpeed;
                animator.SetBool(Animator.StringToHash("isWalking"), false);
                animator.SetBool(Animator.StringToHash("isRunning"), true);
            }

            Vector3 xzDirection = new Vector3(y, 0, -x);
            Quaternion targetRotation = Quaternion.LookRotation(xzDirection);
            targetRotation *= Quaternion.Euler(0, -45, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
            }
        } else {
            animator.SetBool(Animator.StringToHash("isWalking"), false);
            animator.SetBool(Animator.StringToHash("isRunning"), false);
        }
    }
}