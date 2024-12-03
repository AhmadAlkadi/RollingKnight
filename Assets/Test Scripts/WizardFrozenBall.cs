using UnityEngine;

public class WizardFrozenBall : MonoBehaviour
{
    public GameObject frozenBall;
    public Transform frozenBallPos;

    private GameObject player;
    private Animator animator;

    private float timer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update() {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(transform.position);

        if (distance < 10) {
            timer += Time.deltaTime;


            if(timer >= 4) {
            timer = 0;
            TriggerAttack();
        }
        }

        
    }

    void castSpell() {
        Instantiate(frozenBall, frozenBallPos.position, Quaternion.identity);
    }

    void TriggerAttack() {
        animator.SetTrigger("triggerAttack");
    }
}
