using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float moveSpeed = 5;
    [SerializeField] float rateOfFire = 2;
    [SerializeField] float projectileSpeed = 5;
    [SerializeField] int ammo = 100;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;
 
    float lastTimeShotFired;
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        FaceMousePosition();
		Move();
        Shoot();
    }

    private void Shoot(){
        if (Input.GetMouseButton(0)){
            if (Time.time - lastTimeShotFired > rateOfFire && ammo > 0)
        {
            lastTimeShotFired = Time.time;
            ammo--;
            FireProjectile();
        }
        }
        
    }

    private void FireProjectile()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 unitVectortoMousePosition = (worldPoint - transform.position).normalized;
        GameObject projectileObject = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.GetComponent<Rigidbody2D>().velocity = unitVectortoMousePosition * projectileSpeed;
        Destroy(projectile, 5f);
    }

    private void Move()
    {
		float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed;
		float verticalMovement = Input.GetAxis("Vertical") * moveSpeed;
		rb.velocity = new Vector2(horizontalMovement, verticalMovement);
        rb.angularVelocity = 0;
    }

    private void FaceMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        var direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = direction;
    }
}
