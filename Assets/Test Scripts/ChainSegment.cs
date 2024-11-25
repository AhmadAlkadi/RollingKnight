using UnityEngine;

public class ChainSegment : MonoBehaviour
{
    public GameObject connectedAbove;
    public GameObject connectedBelow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetAnchor();
    }

    public void ResetAnchor()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        ChainSegment above_segment = connectedAbove.GetComponent<ChainSegment>();

        if (above_segment != null)
        {
            above_segment.connectedBelow = gameObject;
            float sprite_bottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0.0f, sprite_bottom * -1.0f);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0.0f, 0.0f);
        }
    }
}
