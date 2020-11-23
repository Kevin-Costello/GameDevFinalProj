using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    public Camera camera;
    public GunScriptObj currentGun;
    public Transform bulletInstantiatePosition;
    public bool shooting;
    public bool readyToShoot;
    public bool reloading;
    public int currentAmmo;
    public int bulletsShot;
    float x;
    public float maxGunAngle, gunAngle;
    private bool allowInvoke = true;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize current ammo to the maximum amount
        currentAmmo = currentGun.ammoCapacity;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);
        x = Input.GetAxisRaw("Horizontal");
    
        /*
        transform.localRotation = Quaternion.Euler(0, gunAngle, 0);

        //Moving Right
        if (Mathf.Abs(gunAngle) < maxGunAngle && x > 0)
        {
            gunAngle -= Time.deltaTime * maxGunAngle * 2;
        }

        //Moving Left
        if (Mathf.Abs(gunAngle) < maxGunAngle && x < 0)
        {
            gunAngle += Time.deltaTime * maxGunAngle * 2;
        }

        if (gunAngle > 0 && x == 0)
        {
            gunAngle -= Time.deltaTime * maxGunAngle * 2;
        }

        if (gunAngle < 0 && x == 0)
        {
            gunAngle += Time.deltaTime * maxGunAngle * 2;
        }
        */

        //Reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < currentGun.ammoCapacity && !reloading)
        {
            Reload();
        }

        //If you are out of ammo and try to shoot, reload
        if(readyToShoot && shooting && !reloading && currentAmmo == 0)
        {
            Reload();
        }

        //Shoot
        if (readyToShoot && shooting && !reloading && currentAmmo > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find hit position using raycast
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ray through middle of screen
        RaycastHit hit;
        Vector3 targetPoint;

        //Check if the ray hits something
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            //If the ray doesnt hit anything (shooting in the sky or something) make the point far away
            targetPoint = ray.GetPoint(75);
        }

        //Calculate direction from playerpoint to targetpoint
        Vector3 directionToTarget = targetPoint - bulletInstantiatePosition.position;

        //Calculate gun spread
        float ySpread = Random.Range(-currentGun.spread, currentGun.spread);
        float xSpread = Random.Range(-currentGun.spread, currentGun.spread);

        //Calculate direction with spread
        Vector3 directionWithSpread = directionToTarget + new Vector3(xSpread, ySpread, 0);

        //Instantiate Bullet
        GameObject currentBullet = Instantiate(currentGun.bullet, bulletInstantiatePosition.position, Quaternion.identity);

        //Rotate the bullet in the direction its shooting
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add force to bullets
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * currentGun.shotForce, ForceMode.Impulse);

        currentAmmo --;
        bulletsShot++;
        if(allowInvoke)
        {
            Invoke("ResetShot", currentGun.rateOfFire);
            allowInvoke = false;
        }

        if(bulletsShot < currentGun.bulletsPerShot)
        {
            Invoke("Shoot", 0);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        bulletsShot = 0;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinish", currentGun.reloadTime);
    }

    private void ReloadFinish()
    {
        currentAmmo = currentGun.ammoCapacity;
        reloading = false;
    }
}
