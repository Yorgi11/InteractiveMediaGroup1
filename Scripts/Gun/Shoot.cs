using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private bool canShoot = true;
    private bool isReloading;

    //private Animations animations;
    public void ShootXY(float bulletForce, float shootDelay, Bullet bullet, GameObject bulletSpawn/*, Animations ani*/)
    {
        Bullet currentBullet = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody2D>().AddForce(bulletForce * bulletSpawn.transform.up, ForceMode2D.Impulse);
        //animations = ani;
        StartCoroutine(DelayShooting(shootDelay));
    }
    public void Shoot3D(float bulletForce, float shootDelay, Bullet bullet, GameObject bulletSpawn/*, Animations ani*/)
    {
        Bullet currentBullet = Instantiate(bullet, bulletSpawn.transform.position, transform.rotation);
        currentBullet.GetComponent<Rigidbody>().AddForce(bulletForce * bulletSpawn.transform.forward, ForceMode.Impulse);
        //animations = ani;
        StartCoroutine(DelayShooting(shootDelay));
    }
    public void AimAtMouse2D(Camera cam, GameObject gunPivot, GameObject mouseObject)
    {
        if (mouseObject != null) mouseObject.transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0f);
        Vector3 difference = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gunPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90f);
    }
    IEnumerator DelayShooting(float time)
    {
        //animations.isShooting(true);
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
        //animations.isShooting(false);
    }

    public void Reload(float reloadDelay)
    {
        isReloading = true;
        StartCoroutine(DelayReloading(reloadDelay));
    }
    IEnumerator DelayReloading(float time)
    {
        //animations.isShooting(true);
        yield return new WaitForSeconds(time);
        isReloading = false;
        //animations.isShooting(false);
    }

    public void SetCanShoot(bool state)
    {
        canShoot = state;
    }
    public bool GetCanShoot()
    {
        return canShoot;
    }
    public bool GetIsReloading()
    {
        return isReloading;
    }
}
