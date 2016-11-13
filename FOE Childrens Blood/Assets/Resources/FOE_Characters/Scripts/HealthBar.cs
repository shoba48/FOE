using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    Slider s;
    Image fill;
    Attributes attr;
	// Use this for initialization
	void Start () {
        s = GetComponent<Slider>();
        fill = transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>();
        attr = GetComponentInParent<Attributes>();
        attr.gameObject.GetComponentInChildren<Text>().text = attr.gameObject.name;
	}
	
	// Update is called once per frame
	void Update ()
    {
        s.maxValue = attr.maxLifePoints;
        s.value = attr.LifePoints;
        if (attr.LifePoints == 0) fill.enabled = false;
        else fill.enabled = true;

	}
}
