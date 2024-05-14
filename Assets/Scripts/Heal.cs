using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] int healAmoount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>().isHealthMax()) { return; }
        other.GetComponent<Player>().PlayerHeal(healAmoount);
        Destroy(gameObject);
        //Play sound
    }
}
