using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class VampireBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Line of Sight")]
    public bool hasLOS;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLOS)
        {
            agent.SetDestination(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) < 3)
            {
                animator.SetInteger("AnimState", (int)CryptoState.IDLE);
                transform.LookAt(transform.position - player.transform.forward);

                if (agent.isOnOffMeshLink)
                {
                    animator.SetInteger("AnimState", (int)CryptoState.JUMP);
                }
            }
            else
            {
                animator.SetInteger("AnimState", (int)CryptoState.RUN);
            }
        }
        else
        {
            animator.SetInteger("AnimState", (int)CryptoState.IDLE);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasLOS = true;
            player = other.transform.gameObject;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.transform.gameObject;
        }
    }
}