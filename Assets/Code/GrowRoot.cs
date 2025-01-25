using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowRoot : MonoBehaviour
{
    [SerializeField] private int rootAmount = 2;
    [SerializeField] private float rootGap = .1f;
    [SerializeField] private float startGap = .1f;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GameObject root;
    [SerializeField] private Sprite[] rootImages;
    [SerializeField] private Transform rootContainer;
    [SerializeField] private Transform planetDepth;
    // Start is called before the first frame update
    void Start()
    {
   
        Vector3 currentPos = new(
            rootTransform.position.x,
            rootTransform.position.y,
            planetDepth.position.z
        );

        var growingDir = -transform.up;
        for (int i = 0; i < rootAmount; i++)
        {
            var gapDir = growingDir.normalized * (i == 0 ? startGap : rootGap) * (i + 1);
            GameObject gobRoot; 
            gobRoot = Instantiate(root, currentPos + gapDir, transform.rotation) as GameObject;
            
            gobRoot.GetComponent<SpriteRenderer>().sprite = rootImages[i % 2];
            if (i == rootAmount - 1) 
                gobRoot.GetComponent<SpriteRenderer>().sprite = rootImages[rootImages.Length - 1];
            // Inverting images zig zig mode
            gobRoot.GetComponent<SpriteRenderer>().flipX = i % 2 == 0;
            gobRoot.transform.parent = rootContainer;
        }

    }

    void OnDrawGizmos()
    {
        // Debug.DrawRay(transform.position, )
    }

    // Update is called once per frame
    void Update()
    {

    }
}
