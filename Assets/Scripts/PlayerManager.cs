using UnityEngine;
using System.Collections;

public class PlayerManager : Singleton<PlayerManager> {
    public Health health = new Health(100f);
    public Energy energy = new Energy(100f);

    void Update() {
        energy.Charge(); // FIXME: Can't Energy handle this internally? Unity doesn't seem to like Energy deriving from MonoBehavior, beacuse it not attached to anything
    }
}
