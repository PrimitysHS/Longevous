using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;

    public Transform spawnPoint;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetRun()
    {
        // recarrega a cena e reseta ao carregar
        SceneManager.sceneLoaded += OnSceneLoaded_Reset;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded_Reset(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded_Reset;

        // pega o spawn da cena recarregada
        GameObject sp = GameObject.Find("SpawnPoint");
        if (sp != null) spawnPoint = sp.transform;

        // pega player
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p == null) return;

        // teleporta pro spawn
        if (spawnPoint != null)
            p.transform.position = spawnPoint.position;

        // reseta HP
        PlayerHealth ph = p.GetComponent<PlayerHealth>();
        if (ph != null)
            ph.currentHP = ph.maxHP;

        // reseta sangue
        PlayerBlood pb = p.GetComponent<PlayerBlood>();
        if (pb != null)
            pb.currentBlood = 0;

        // reseta upgrades
        PlayerUpgrades pu = p.GetComponent<PlayerUpgrades>();
        if (pu != null)
        {
            pu.burnOnHit = false;
            pu.lifestealOnKill = false;
            pu.critChance = false;
        }
    }
}