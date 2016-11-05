using UnityEngine;

public class HumanControls : MonoBehaviour {
    private float normalSpeed = 10f;
    private float turnSpeed = 360f; // Degrees per second
    private float boostSpeed = 20f;
    public bool controlsEnabled = true;

    private Animator animator;

    private void Awake() {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void Walk() {
        animator.SetBool(Animator.StringToHash("isWalking"), true);
        animator.SetBool(Animator.StringToHash("isRunning"), false);
    }

    public void Run() {
        animator.SetBool(Animator.StringToHash("isWalking"), false);
        animator.SetBool(Animator.StringToHash("isRunning"), true);
    }

    public void Stop() {
        animator.SetBool(Animator.StringToHash("isWalking"), false);
        animator.SetBool(Animator.StringToHash("isRunning"), false);
    }

    private void Update() {
        if (!GameManager.Instance.gameActive) {
            Stop();
            return;
        }

        if (!controlsEnabled) { return; }
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0) {
            float speed = normalSpeed;
            Walk();

            if (Input.GetKey(KeyCode.LeftShift)) {
                speed = boostSpeed;
                Run();
            }

            Vector3 xzDirection = new Vector3(x, 0f, y);
            Quaternion targetRotation = Quaternion.LookRotation(xzDirection);
            //targetRotation *= Quaternion.Euler(0, -45, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f) {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
            }
        } else {
            Stop();
        }
    }
}