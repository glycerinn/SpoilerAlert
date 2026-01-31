using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Drawing;

public class EnemyBehaviour : MonoBehaviour
{
    public EnemySO EnemySO;
    public int CurrentHealth;

    [SerializeField] private GameObject SpoilerBarPrefab;
    [SerializeField] private Vector3 SpoilerBarOffset = new Vector3(0, 0.5f, 0);

    private GameObject SpoilerBarInstance;
    private Slider SpoilerSlider;
    private Coroutine SpoilerRoutine;
    private PathPoint pathPoint;
    private Bullets ammo;

    public void Awake()
    {
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
        if(CurrentHealth <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            StopSpoiler();
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (ammo == null)
            return;

        if (!ammo.CanConsumeAmmo())
            return;

        ammo.ShootGun();
        ammo.ConsumeAmmo();
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
}
