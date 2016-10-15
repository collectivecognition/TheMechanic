using UnityEngine;
using System.Collections;

public class TankAI : MonoBehaviour {
    private TankTurret turret;
    private TankGun gun;

    void Start() {
        turret = GetComponent<TankTurret>();
        gun = GetComponent<TankGun>();
    }

    void Update() {
        if(name != "Player" && BattleManager.Instance.BattleActive) {
           turret.AimAt(GameManager.Instance.player.transform.position);

            // If we're aimed

            if (turret.IsAimingAt(GameManager.Instance.player.transform.position)) {
                gun.Fire();
            }
        }
    }
}
