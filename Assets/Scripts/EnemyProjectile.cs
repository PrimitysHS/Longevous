using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 6f;
    public int damage = 20;
    public float lifeTime = 4f;

    private Vector2 direction;

    public void Init(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}