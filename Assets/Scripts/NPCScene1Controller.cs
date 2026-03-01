using UnityEngine;
using UnityEngine.AI;

public class NPCScene1Controller : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public Transform player;
    public float moveSpeed = 3f;
    public float stoppingDistance = 2.5f;

    private bool playerNearby = false;
    public bool Stopped = false;
    public bool dialogueStarted = false;

    public GameObject dialogueUI;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        dialogueUI.SetActive(false);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = stoppingDistance;
        navMeshAgent.speed = moveSpeed;
    }

    void Update()
    {
        if (playerNearby && !Stopped)
        {
            MoveTowardsPlayer();
        }

        if (FindObjectOfType<DialogueManager>() == null && Stopped && dialogueStarted)
        {
            gameObject.SetActive(false);
            playerMovementScript.ResumeMovement();

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
           // other.GetComponent<Animator>().enabled = false;
           // other.GetComponent<CharacterController>().enabled = false;
        }
    }

    void MoveTowardsPlayer()
    {
        navMeshAgent.SetDestination(player.position);

        if (navMeshAgent.remainingDistance <= stoppingDistance)
        {
            Stopped = true;
            dialogueUI.SetActive(true);
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
            dialogueStarted = true;
            navMeshAgent.isStopped = true; 
            playerMovementScript.FreezeMovement();
        }
    }
}
