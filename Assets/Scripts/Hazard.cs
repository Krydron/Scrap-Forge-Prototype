using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int hazardDamage;
    private Vector3 difference;
    [SerializeField] float knockbackModifier;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Deal Damage
        other.GetComponent<Player>().PlayerDamage(hazardDamage);

        //Knockback player (Not working)
        difference = (transform.position - other.transform.position).normalized;
        other.GetComponent<Rigidbody>().AddForce(difference*knockbackModifier);

        //Play sound
    }
}
