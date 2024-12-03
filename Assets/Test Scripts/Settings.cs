using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public MenuMove lockMenuMovement;
    public Canvas parentOfText;
    public bool hit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (collision.gameObject.CompareTag("PlayerAttackBox") && !hit)
            {
                Invoke(nameof(SettingsInvokeEnable), 0.4f);
            }
            else if (collision.gameObject.CompareTag("PlayerAttackBox") && hit)
            {
                Invoke(nameof(SettingsInvokeDisable), 0.4f);
            }
        }
    }

    private void SettingsInvokeEnable()
    {
        if (!hit)
        {
            lockMenuMovement.enabled = false;
            parentOfText.transform.GetChild(0).gameObject.SetActive(false);
            parentOfText.transform.GetChild(1).gameObject.SetActive(false);
            parentOfText.transform.GetChild(2).gameObject.SetActive(true);
            parentOfText.transform.GetChild(3).gameObject.SetActive(false);
            parentOfText.transform.GetChild(4).gameObject.SetActive(true);
            hit = true;
        }
    }


    private void SettingsInvokeDisable()
    {
        if (hit)
        {
            lockMenuMovement.enabled = true;
            parentOfText.transform.GetChild(0).gameObject.SetActive(true);
            parentOfText.transform.GetChild(1).gameObject.SetActive(true);
            parentOfText.transform.GetChild(2).gameObject.SetActive(false);
            parentOfText.transform.GetChild(3).gameObject.SetActive(true);
            parentOfText.transform.GetChild(4).gameObject.SetActive(false);
            hit = false;

        }
    }
}
