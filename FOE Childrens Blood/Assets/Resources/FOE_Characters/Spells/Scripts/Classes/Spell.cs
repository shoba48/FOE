using UnityEngine;
using System.Collections;
using System;

public class Spell : MonoBehaviour
{
    public float awakeTime;
    public float duration = 5;
    public GameObject creator = null;
    public GameObject victim = null;
    public KeyCode fireKey;
    


    public virtual void Awake()
    {
        
        awakeTime = Time.time; 
    }

    public virtual void Update()
    {
        if (Time.time - awakeTime > duration)
            EndSpell();
    }

    public virtual void EndSpell()
    {
        GameObject.Destroy(this.gameObject);
    }
}
