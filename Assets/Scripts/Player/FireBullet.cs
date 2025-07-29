using System.Collections;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 90;
    [SerializeField] private float disableAfter = 5f;

    private Animator _animator;
    private bool canMove;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        canMove = true;
        StartCoroutine(DisableBullet(disableAfter));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    private void OnTriggerEnter(Collider target)
    {

        if (target.CompareTag("Enemy"))
        {
            target.gameObject.GetComponent<EnemyLife>().TakeDamage(damage);
            _animator.Play("Explode");
            speed = 0f;
            StartCoroutine(DisableBullet(1f));
        }

        if (target.CompareTag("House"))
        {
            // not implemented yet
        }
    }

    private IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}