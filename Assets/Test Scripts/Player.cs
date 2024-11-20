using UnityEngine;

public class Player: MonoBehaviour
{
    public PlayerSpriteRenderer normalRenderer;

    private DeathAnimation deathAnimation;

    public bool normal => normalRenderer.enabled;

    private void Awake(){
        deathAnimation = GetComponent<DeathAnimation>();
    }
    public void Hit(){
        Death();
    }

    private void Death() {
        normalRenderer.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
    }
}
