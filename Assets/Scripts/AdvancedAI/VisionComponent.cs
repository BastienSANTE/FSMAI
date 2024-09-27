using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{

    public string visionTag;
    public List<GameObject> inVision = new();
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(visionTag)) return;
        inVision.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(visionTag) && inVision.Contains(other.gameObject)) return;
        inVision.Remove(other.gameObject);
    }
    
}
