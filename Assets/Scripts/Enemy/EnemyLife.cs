using System.Collections;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public static EnemyLife Instance;

    [SerializeField] private int maxHealth = 90;
    [SerializeField] private int currentHealth;

    private Animator anim;
    public string explosionTag = "ExplodedEnemy"; // The tag you want to set on the enemy after the explosion.

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int Amount)
    {
        currentHealth -= Amount;
        if (currentHealth <= 0)
        {
            DeactivateScript();
        }
    }

    public void DeactivateScript()
    {
        if (string.IsNullOrEmpty(explosionTag))
        {
            Debug.LogError("Explosion tag is not defined.");
            return;
        }
        StartCoroutine(DeactivateEnemyGameObject());
    }


    IEnumerator DeactivateEnemyGameObject()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            Physics2D.IgnoreCollision(collider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), true);
        }

        anim.Play("Explossion");

        gameObject.tag = explosionTag;

        yield return new WaitForSeconds(2f);

        // Notify SpawnZombie to spawn a new enemy
        SpawnZombie.instance.OnEnemyKilled();

        foreach (Collider2D collider in colliders)
        {
            Physics2D.IgnoreCollision(collider, GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), false);
        }

        Destroy(gameObject);
    }


}
