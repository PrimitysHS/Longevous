using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private int baseDamage = 30;
    private float hitstopDuration = 0.07f;

    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;

    private HashSet<int> hitThisSwing = new HashSet<int>();

    void OnEnable()
    {
        hitThisSwing.Clear();
    }

    public void SetDamage(int dmg, float hitstop)
    {
        baseDamage = dmg;
        hitstopDuration = hitstop;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        int id = other.gameObject.GetInstanceID();
        if (hitThisSwing.Contains(id)) return;
        hitThisSwing.Add(id);

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy == null) return;

        PlayerUpgrades upgrades = transform.root.GetComponent<PlayerUpgrades>();

        int finalDamage = baseDamage;
        bool wasCrit = false;

        if (upgrades != null && upgrades.critChance)
        {
            float roll = Random.Range(0f, 100f);
            if (roll <= upgrades.critPercent)
            {
                finalDamage = Mathf.RoundToInt(finalDamage * upgrades.critMultiplier);
                wasCrit = true;
            }
        }

        if (upgrades != null && upgrades.burnOnHit)
            finalDamage += 10;

        enemy.TakeDamage(finalDamage, (Vector2)transform.root.position);

        SpawnDamagePopup(other.transform.position, finalDamage, wasCrit);

        // ✅ HITSTOP no Player (objeto não desativa)
        HitstopManager hm = transform.root.GetComponent<HitstopManager>();
        if (hm != null) hm.DoHitstop(hitstopDuration);
    }

    void SpawnDamagePopup(Vector3 enemyPos, int dmg, bool crit)
    {
        if (damagePopupPrefab == null) return;

        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;

        Camera cam = Camera.main;
        if (cam == null) return;

        GameObject go = Instantiate(damagePopupPrefab, canvas.transform);
        go.transform.position = cam.WorldToScreenPoint(enemyPos + Vector3.up * 0.8f);

        DamagePopup dp = go.GetComponent<DamagePopup>();
        if (dp != null)
            dp.SetText(crit ? $"{dmg}!" : $"{dmg}");
    }
}