﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){
		DamageSystem damageSystem = col.GetComponent<DamageSystem>();
		if (transform.parent != col.transform.parent && damageSystem){
			damageSystem.TakeDamage();
		}
		Destroy(gameObject);
	}
}
