using UnityEngine;

public class ChainPulley : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabChainSegs;
    public int numLinks = 5;

    public HingeJoint2D top;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateChain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateChain()
    {
        Rigidbody2D prev_body = hook;

        for (int i=0; i<numLinks; i++)
        {
            int index = Random.Range(0, prefabChainSegs.Length);

            GameObject new_segment = Instantiate(prefabChainSegs[index]);

            new_segment.transform.parent = transform;
            new_segment.transform.position = transform.position;
            HingeJoint2D hj = new_segment.GetComponent<HingeJoint2D>();
            hj.connectedBody = prev_body;

            prev_body = new_segment.GetComponent<Rigidbody2D>();

            if (i == 0)
            {
                top = hj;
            }
        }
    }

    public void addLink()
    {
        int index = Random.Range(0, prefabChainSegs.Length);
        GameObject new_segment = Instantiate(prefabChainSegs[index]);

        new_segment.transform.parent = transform;
        new_segment.transform.position = transform.position;
        HingeJoint2D hj = new_segment.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;

        new_segment.GetComponent<ChainSegment>().connectedBelow = top.gameObject;
        top.connectedBody = new_segment.GetComponent<Rigidbody2D>();
        top.GetComponent<ChainSegment>().ResetAnchor();

        top = hj;
    }

    public void removeLink()
    {
        HingeJoint2D new_top = top.gameObject.GetComponent<ChainSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        new_top.connectedBody = hook;
        new_top.gameObject.transform.position = hook.gameObject.transform.position;
        new_top.GetComponent<ChainSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = new_top;
    }

}
