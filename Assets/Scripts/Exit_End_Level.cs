using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit_End_Level : MonoBehaviour
{
    public string level = "";
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SceneManager.LoadScene(level);
        }
    }
}