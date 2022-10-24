using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float bulletForce;
    [SerializeField] private float shootDelay;
    [SerializeField] private float reloadDelay;

    [SerializeField] private int maxAmmo;

    [SerializeField] private GameObject mouseObject;
    [SerializeField] private GameObject bulletSpawn;

    [SerializeField] private Bullet bullet;

    [Header("1=Semi, 2=Auto, 3=Burst")]
    [SerializeField][Range(1, 3)] private int fireMode = 1;

    private int currentAmmo;

    private bool shoot;
    private bool isShoot;
    private bool canReload = true;

    private Shoot gun;

    private Camera cam;
    void Start()
    {
        currentAmmo = maxAmmo;
        gun = GetComponent<Shoot>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (fireMode == 1 || fireMode == 3) if (Input.GetKeyDown(KeyCode.Mouse0)) shoot = true;
        if (fireMode == 2) if (Input.GetKey(KeyCode.Mouse0)) shoot = true;
        if (Input.GetKeyUp(KeyCode.Mouse0)) shoot = false;

        if (currentAmmo <= 0 && canReload)
        {
            gun.Reload(reloadDelay);
            canReload = false;
        }
        if (!canReload && !gun.GetIsReloading())
        {
            currentAmmo = maxAmmo;
            canReload = true;
        }
        // Aim at the mouse
        gun.AimAtMouse2D(cam, this.gameObject, mouseObject);
    }

    public void FixedUpdate()
    {
        if (shoot && gun.GetCanShoot() && currentAmmo > 0)
        {
            gun.ShootXY(bulletForce, shootDelay, bullet, bulletSpawn);
            shoot = false;
            currentAmmo--;
        }
    }
}
