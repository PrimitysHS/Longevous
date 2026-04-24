using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public float shootRange = 7f;
    public float shootCooldown = 1.4f;

    public Transform shootPoint;
    public GameObject projectilePrefab;

    private Transform player;
    private float lastShootTime;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;
        if (shootPoint == null) return;
        if (projectilePrefab == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= shootRange)
            TryShoot();
    }

    void TryShoot()
    {
        if (Time.time - lastShootTime < shootCooldown) return;
        lastShootTime = Time.time;

        Vector2 dir = (player.position - shootPoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
        if (ep != null)
            ep.Init(dir);
        else
            Destroy(proj); // se o prefab estiver sem script, não deixa lixo na cena
    }
}