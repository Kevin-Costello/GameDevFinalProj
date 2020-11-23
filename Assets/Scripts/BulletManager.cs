using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    private GunScriptObj currentGun;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        currentGun = GameObject.Find("Gun").GetComponent<GunController>().currentGun;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!currentGun.isBouncing)
        {
            if(collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            {
                Destroy(gameObject);
            }
        }
        if(currentGun.isBouncing)
        {
            if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9)
            {
                rb.AddForce(collision.contacts[0].normal * 1000);
            }
        }

    }
}
