using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int MaxAmmo = 20;
    [SerializeField] private int CurrentAmmo;
    [SerializeField] private float ReloadTime = 1f;
    [SerializeField] private Slider AmmoBar;

    public Animator animator;
    private bool isReloading;

    private void Start()
    {   
        CurrentAmmo = MaxAmmo;
        AmmoBar.maxValue = MaxAmmo;
        AmmoBar.value = CurrentAmmo;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && CanConsumeAmmo())
        {
            ConsumeAmmo();
            ShootGun();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isReloading)
        {
            StartCoroutine(ReloadAmmo());
        }

        AmmoBar.value = CurrentAmmo;
    }

    public bool CanConsumeAmmo()
    {
        return CurrentAmmo > 0 && !isReloading;
    }

    public void ConsumeAmmo()
    {
        if (!CanConsumeAmmo())
            return;

        CurrentAmmo--;
    }

    private IEnumerator ReloadAmmo()
    {
        isReloading = true;

        yield return new WaitForSeconds(ReloadTime);

        CurrentAmmo = MaxAmmo;
        isReloading = false;
    }

    public void ShootGun()
    {
        animator.ResetTrigger("Shoot");
        animator.SetTrigger("Shoot");
    }
}
