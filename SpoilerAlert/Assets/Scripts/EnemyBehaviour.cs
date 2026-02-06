using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemySO EnemySO;
    public int CurrentHealth;

    [SerializeField] private GameObject SpoilerBarPrefab;
    [SerializeField] private Vector3 SpoilerBarOffset = new Vector3(0, 0.6f, 0);
    [SerializeField] private float deathAnimDuration = 0.6f;

    private Animator animator;
    private AudioManager audioManager;
    private GameObject SpoilerBarInstance;
    private Slider SpoilerSlider;
    private Coroutine SpoilerRoutine;
    private PathPoint pathPoint;
    private Bullets ammo;
    private bool isDying;
    private SpriteRenderer rend;

    private static readonly int MoveX = Animator.StringToHash("DirectionX");
    private static readonly int MoveY = Animator.StringToHash("DirectionY");
    private static readonly int Move = Animator.StringToHash("Moving");

    public void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = EnemySO.animatorController;
        CurrentHealth = EnemySO.MaxEnemyHealth;
        ammo = FindAnyObjectByType<Bullets>();
        SpoilerBarInstance = Instantiate(
            SpoilerBarPrefab,
            transform.position + SpoilerBarOffset,
            Quaternion.identity,
            transform
        );

        SpoilerSlider = SpoilerBarInstance.GetComponentInChildren<Slider>();
        SpoilerSlider.value = 0f;

        SpoilerBarInstance.SetActive(false);
    }

    public void Update()
    {
        if(CurrentHealth <= 0 && !isDying)
        {
            Death();
        }
    }

    private void OnMouseDown()
    {
        if (ammo == null)
            return;

        if (!ammo.CanConsumeAmmo())
            return;
        StartCoroutine(FlashHit());
        audioManager.playHitSFX();
        takeDamage(1);
    }

    public void takeDamage(int damage)
    {
        CurrentHealth -= damage;
    }

    public void showSpoilerBar(PathPoint point)
    {
        pathPoint = point;

        if (SpoilerRoutine != null)
            StopCoroutine(SpoilerRoutine);

        SpoilerBarInstance.SetActive(true);
        SpoilerSlider.value = 0f;

        SpoilerRoutine = StartCoroutine(Spoiling());
    }

    private IEnumerator Spoiling()
    {
        float timer = 0f;

        while (timer < EnemySO.SpoilerSpeed)
        {
            timer += Time.deltaTime;
            SpoilerSlider.value = timer / EnemySO.SpoilerSpeed;
            yield return null;
        }

        SpoilerSlider.value = 1f;
        OnSpoilerCharged();
    }

    private void OnSpoilerCharged()
    {
        if(pathPoint != null)
            pathPoint.Spoiled = true;
    }

    private void StopSpoiler()
    {
        if (SpoilerRoutine != null)
            StopCoroutine(SpoilerRoutine);

        SpoilerSlider.value = 0f;
        SpoilerBarInstance.SetActive(false);
    }

    public void UpdateAnimation(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.001f)
        {
            animator.SetBool(Move, false);
            return;
        }

        animator.SetBool(Move, true);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animator.SetFloat(MoveX, Mathf.Sign(direction.x));
            animator.SetFloat(MoveY, 0);
        }
        else
        {
            animator.SetFloat(MoveX, 0);
            animator.SetFloat(MoveY, Mathf.Sign(direction.y));
        }
    }

    private void Death()
    {
        isDying = true;

        StopSpoiler();

        animator.SetTrigger("Die");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        CircleCollider2D col = GetComponent<CircleCollider2D>();
        if (col != null)
            col.enabled = false;

        StartCoroutine(DestroyAfterDeath());
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(deathAnimDuration);
        Destroy(gameObject);
    }

    private IEnumerator FlashHit()
    {
        rend.color = Color.pink;
        yield return new WaitForSeconds(0.5f);
        rend.color = Color.white;
    }
}
