using UnityEngine;

public class myFlavorTrigger : MonoBehaviour
{
    private void Start()
    {
        Object.FindFirstObjectByType<myFlavorManager>()?._collectedFlavor();
        Destroy(gameObject, 3f);
    }
}
