using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform crouchFirePoint;
    [SerializeField] private Transform runFirePoint;
    [SerializeField] private GameObject bulletPrefab;

    private void Shoot()
    {
        SoundManager.Instance.Play(Sounds.Bullet);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    private void CrouchShoot()
    {
        SoundManager.Instance.Play(Sounds.Bullet);
        Instantiate(bulletPrefab, crouchFirePoint.position, crouchFirePoint.rotation);
    }
    private void RunShoot()
    {
        SoundManager.Instance.Play(Sounds.Bullet);
        Instantiate(bulletPrefab, runFirePoint.position, runFirePoint.rotation);
    }
}
