using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 250;
    public int currentHP;

    [Header("Knockback")]
    public float knockbackForceX = 8f;
    public float knockbackForceY = 3f;

    private Rigidbody2D rb;

    void Awake()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        // Defesa (botão direito segurado)
        PlayerDefense defense = GetComponent<PlayerDefense>();
        if (defense != null)
            damage = defense.ModifyDamage(damage);

        // Aplica knockback quando toma hit
        ApplyKnockback();

        // Aplica dano
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        // Morreu -> reset total da run
        if (currentHP <= 0)
        {
            if (RunManager.Instance != null)
                RunManager.Instance.ResetRun();
            else
                Debug.LogError("RunManager.Instance não encontrado na cena.");
        }
    }

    void ApplyKnockback()
    {
        if (rb == null) return;

        // Direção baseada no lado que o player está virado (scale.x)
        float dir = Mathf.Sign(transform.localScale.x);

        // Zera velocidade antes pra ficar seco
        rb.velocity = new Vector2(0f, rb.velocity.y);

        rb.AddForce(new Vector2(-dir * knockbackForceX, knockbackForceY), ForceMode2D.Impulse);
    }
}