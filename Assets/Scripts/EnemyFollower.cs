using UnityEngine;
using System.Collections;

public class EnemyFollower : Enemy {
    private Transform player;

    private float followSpeed = 5f;

	void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	void Update () {
        Vector3 towardsPlayer = Towards(transform.position, player.position);
        transform.position = Vector3.MoveTowards(transform.position, towardsPlayer, followSpeed * Time.deltaTime);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3[] awayFromEnemies = new Vector3[enemies.Length];

        for(int ii = 0; ii < enemies.Length; ii++) {
            GameObject enemy = enemies[ii];
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if(distance == 0 || distance > 4) { // Ignore self and far away enemies
                awayFromEnemies[ii] = Vector3.zero;
            }else{
                awayFromEnemies[ii] = -Towards(transform.position, enemy.transform.position) * distance * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, awayFromEnemies[ii], 1f * Time.deltaTime);
            }
        }


	}

    private Vector3 Towards(Vector3 a, Vector3 b) {
        Vector3 result = Vector3.MoveTowards(a, b, 1f);
        result.y = a.y;
        return result;
    }
}
