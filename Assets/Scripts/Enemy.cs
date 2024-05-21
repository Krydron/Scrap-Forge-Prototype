using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemyState
    {
        patrolling,
        alert,
        dead,
        attacking,
        idle
    }

    public GameObject player;
    public GameObject AlertSprite; //gameobject exclaimation mark
    public List<Vector3> patrolWaypoints; //list of waypoints to patrol

    private EnemyState currentState = EnemyState.patrolling; //current AI state
    private int currentWaypointIndex = 0; //index of currently targeted waypoint
    private float currentPatrolTime = 0; //used for lerping
    float patrolTime; //time taken to patrol between waypoints
    const float patrolSpeed = 1.5f; //speed at which to patrol

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.patrolling) //if should be patrolling
        {
            if ((patrolWaypoints[currentWaypointIndex] - transform.position).magnitude >= 0.05f)
            {
                Vector3 newEnemyPos = patrolWaypoints[mod((currentWaypointIndex - 1), patrolWaypoints.Count)]; //get prev waypoint pos
                currentPatrolTime += Time.deltaTime; //increment lerp counter
                float lerpFac = currentPatrolTime/patrolTime; //calculate lerp factor
                newEnemyPos = new Vector3(Mathf.Lerp(newEnemyPos.x, patrolWaypoints[currentWaypointIndex].x, lerpFac),
                    Mathf.Lerp(newEnemyPos.y, patrolWaypoints[currentWaypointIndex].y, lerpFac),
                    Mathf.Lerp(newEnemyPos.z, patrolWaypoints[currentWaypointIndex].z, lerpFac)); //lerp between last and next waypoint
                transform.position = newEnemyPos; //update position
            }
            else
            {
                currentWaypointIndex = mod(currentWaypointIndex + 1, patrolWaypoints.Count); //increment index and wrap
                float patrolDistance = (patrolWaypoints[currentWaypointIndex] - patrolWaypoints[mod((currentWaypointIndex - 1),
                    patrolWaypoints.Count)]).magnitude; //calculate distance between
                patrolTime = patrolDistance/patrolSpeed; //calulate time required to move
                currentPatrolTime = 0f; //reset counter
                transform.LookAt(patrolWaypoints[currentWaypointIndex], Vector3.up); //look at next waypoint
            }
        }
        if (currentState == EnemyState.attacking)
        {
            if (!AlertSprite.activeSelf) //if excplaimation mark isn't active
            {
                AlertSprite.SetActive(true); //set active
            }
            AlertSprite.transform.LookAt(Camera.main.transform.position);
            transform.LookAt(player.transform.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            Vector3 stepAmount = player.transform.position - transform.position;
            transform.position += new Vector3(stepAmount.x, 0, stepAmount.z).normalized * patrolSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.tag == "Player")
            currentState = EnemyState.attacking;
    }

    private int mod(int x, int m) //modulus function
    {
        return (x%m + m)%m; //negative friendly function
    }
}
