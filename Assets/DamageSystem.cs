using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageSystem : MonoBehaviour {
	[SerializeField] int health = 3;
	[SerializeField] float damageFlashTime = .1f;
	[SerializeField] AudioClip hitSound;
	[SerializeField] AudioClip deathSound;

	AudioSource audioSource;

	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	public void TakeDamage(){
		StartCoroutine(HandleDamage());
	}

	protected IEnumerator HandleDamage()
    {
        
        audioSource.PlayOneShot(hitSound);
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.red;
		health--;
        if (health <= 0)
        {
            Die();
        }
		yield return new WaitForSecondsRealtime(damageFlashTime);
		renderer.color = Color.white;
    }

    private void Die()
    {
		audioSource.PlayOneShot(hitSound);
		audioSource.clip = deathSound;
		audioSource.Play();
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, audioSource.clip.length);
    }
	
}
