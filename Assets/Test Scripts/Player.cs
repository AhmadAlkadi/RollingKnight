using UnityEngine;
using System.Collections;

public class Player: MonoBehaviour
{
    public PlayerSpriteRenderer normalRenderer;
    private HealthManager healthManager;

    private DeathAnimation deathAnimation;
    private bool isVulnerable = true;
    

    public bool normal => normalRenderer.enabled;

    private void Awake(){
        deathAnimation = GetComponent<DeathAnimation>();
        if(GameManager.Instance != null) {
            healthManager = GameManager.Instance.GetComponent<HealthManager>();
        }

        // Handle missing HealthManager component
        if (healthManager == null) {
            Debug.LogError("HealthManager component not found on GameManager!");
        }
    }
    public void Hit(){
        if(isVulnerable) {
            healthManager.health--;
            if(healthManager.health <= 0) {
                Death();
            }else {
                StartCoroutine(GetHurt());
            }
        }
        
    }

    IEnumerator GetHurt(){
        //Layer 3: Player
        //Layer 8: Enemy
        //Enable ignore collision
        isVulnerable = false;
        Physics2D.IgnoreLayerCollision(3,8,true);
        yield return new WaitForSeconds(3);
        //Disable ignore collision
        Physics2D.IgnoreLayerCollision(3,8,false);
        isVulnerable = true;
    }

    private void Death() {
        normalRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
    }
}
