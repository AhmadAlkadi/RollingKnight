using UnityEngine;

public class TowerEntryExit : MonoBehaviour
{
    public GameObject TowerOutside;
    public GameObject TowerInsideBack;
    public GameObject TowerInsideFront;

    [SerializeField]
    private bool withinTowerEntryWay = false; // should be set to private when tested to work

    [SerializeField]
    private bool withinTowerEntryWayKeyPressed = false; // should be set to private when tested to work

    [SerializeField]
    private bool isInsideTower = false; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerInsideFrontColliders; // should be set to private when tested to work

    [SerializeField]
    private BoxCollider2D[] towerInsideBackColliders; // should be set to private when tested to work

    // Check if Player enters the Entry Way of the Tower
    private void OnTriggerEnter2D(Collider2D collision)
    {
        withinTowerEntryWay = true;
    }

    // Check if Player leaves the Entry Way of the Tower
    private void OnTriggerExit2D(Collider2D collision)
    {
        withinTowerEntryWay = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        towerInsideFrontColliders = TowerInsideFront.GetComponents<BoxCollider2D>();
        towerInsideBackColliders = TowerInsideBack.GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float up_key_press = Input.GetAxisRaw("Vertical");

        if ((up_key_press > 0.0f) && withinTowerEntryWay)
        {
            if (!withinTowerEntryWayKeyPressed)
            {
                isInsideTower ^= true;

                TowerOutside.SetActive(!isInsideTower);
                TowerInsideFront.SetActive(isInsideTower);

                foreach (var collider in towerInsideFrontColliders)
                {
                    collider.enabled = isInsideTower;
                }
                
                foreach (var collider in towerInsideBackColliders)
                {
                    collider.enabled = isInsideTower;
                }

                withinTowerEntryWayKeyPressed = true;
            }
        }
        else
        {
            withinTowerEntryWayKeyPressed = false;
        }
    }
}
