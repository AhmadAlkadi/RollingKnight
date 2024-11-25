using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerEntryExit : MonoBehaviour
{
    public GameObject TowerOutside;
    public GameObject TowerOutsideDoorArchway;
    public GameObject TowerInsideBack;
    public GameObject TowerInsideFront;
    public GameObject TowerMasking;
    public float alphaVisibilityOfArchway = 0.5f;
    public float alphaVisibilityOfMasking = 0.5f;

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

    [SerializeField]
    private Material matTowerOutsideDoorArchway;

    [SerializeField]
    private Material matTowerMasking;

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
        matTowerOutsideDoorArchway = TowerOutsideDoorArchway.GetComponent<TilemapRenderer>().material;
        matTowerMasking = TowerMasking.GetComponent<TilemapRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        EnterAndExitTower();
    }

    public void EnterAndExitTower()
    {
        // If the player hits the up-direction then the player should enter the building
        // this is done by making the outside tower grid inactive when inside and active when outside

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

        // show slightly transparent color of the archway/entry to the tower and completely opaque when outside
        // the tower

        if (isInsideTower && matTowerOutsideDoorArchway)
        {
            Color c = matTowerOutsideDoorArchway.color;
            c.a = alphaVisibilityOfArchway;
            matTowerOutsideDoorArchway.color = c;

            Color c_masking = matTowerMasking.color;
            c_masking.a = alphaVisibilityOfMasking;
            matTowerMasking.color = c_masking;
        }
        else if (matTowerOutsideDoorArchway)
        {
            Color c = matTowerOutsideDoorArchway.color;
            c.a = 1.0f;
            matTowerOutsideDoorArchway.color = c;

            Color c_masking = matTowerMasking.color;
            c_masking.a = 0.0f;
            matTowerMasking.color = c_masking;
        }
    }
}
