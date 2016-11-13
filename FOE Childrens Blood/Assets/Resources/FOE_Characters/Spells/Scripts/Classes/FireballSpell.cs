using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;

public class FireballSpell : Spell
{
	Attributes attr;
	FireProjectileScript fps;
	Rigidbody projectilRidgidbody;
	Rigidbody creatorRidgidbody;
	AudioSource[] audioSource;

	float power = 0;
	float cost = 2;
	float fireSpeed = 20;
	private bool fired = false;




	public override void Awake ()
	{
		base.Awake ();
        
		fps = GetComponent<FireProjectileScript> ();
		projectilRidgidbody = GetComponentInChildren<Rigidbody> ();
		projectilRidgidbody.isKinematic = true;
		audioSource = GetComponents<AudioSource> ();
		duration = Mathf.Infinity;
		transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		transform.localPosition = new Vector3 (0, 10f, 1f);
          
	}

	public void Start ()
	{
		attr = creator.GetComponent<Attributes> ();
		creatorRidgidbody = creator.GetComponent<Rigidbody> ();
		AudioSelectByClipName ("FireLoop2").volume = 0.01f;
		if (attr.Mana == 0)
			base.EndSpell ();
	}

	public override void Update ()
	{
		base.Update ();
		if (Input.GetKey (fireKey) && !fired) {
			if (attr.Mana > 0)
			{	
				
					Charge(cost);
			}
			else
				Charge (-1);
		} else
            //if (Input.GetKeyUp(KeyCode.Alpha2))
            Fire ();
	}


	void Charge (float increment)
	{
		if (increment > 0)
		{
			attr.Mana -= Mathf.RoundToInt (increment);
		if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
			attr.Mana -= attr.chargeSpeed;
		}
		power += increment;

		if (power > 0) {
			AudioSelectByClipName ("FireLoop2").volume += increment / 400;

			transform.localScale += new Vector3 (1, 1, 1) * increment / 200;

			fps.ProjectileExplosionForce += 2 * increment;
			fps.ProjectileExplosionRadius = Mathf.Log (power + 500, 1.5f) - 15 + transform.localScale.magnitude; //2*log((1:1500)+500)/log(1.5)-30);
			projectilRidgidbody.mass = 50 * fps.ProjectileExplosionRadius;            
            
			if (increment > 0) { // avoid collision with creator
				transform.localPosition = transform.localScale.x * (Vector3.forward + Vector3.up) + Vector3.forward + Vector3.up;
				// correct the ridgidbody
				projectilRidgidbody.velocity = creatorRidgidbody.velocity;
			}
		} else
			base.EndSpell ();
	}


	void Fire ()
	{
        
		projectilRidgidbody.isKinematic = false;
		if (transform.parent != null)
			AudioSelectByClipName ("FireShoot1").Play ();

		transform.parent = null;
		Vector3 direction;
		if (!fired) {
			if (victim != null)
				direction = (victim.transform.position + victim.transform.up) - transform.position;
			else
				direction = creator.transform.forward;
			fired = true;
		} else
		{
			direction = transform.forward;
		}
		transform.position += direction.normalized * Time.deltaTime * fireSpeed;
		Charge (-1);
	}


	AudioSource AudioSelectByClipName (string clipName)
	{
		foreach (AudioSource aS in audioSource) {
			if (clipName == aS.clip.name)
				return aS;
		}
		return null;
	}

	void OnTriggerEnter (Collider col)
	{
		Debug.Log (col);
	}
}
