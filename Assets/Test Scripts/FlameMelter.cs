using UnityEngine;
using UnityEngine.SceneManagement;

public class FlameMelter : MonoBehaviour
{
    private bool isBurning = false;


    private void OnParticleCollision(GameObject other)
    {
        FlameMelty fm = other.gameObject.GetComponent<FlameMelty>();

        if (fm)
        {
            fm.SetIsBurning(true);
        }
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
