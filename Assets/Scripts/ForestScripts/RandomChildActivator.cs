using UnityEngine;

public class RandomChildActivator : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        int randomIndex = Random.Range(0, 3);
        transform.GetChild(randomIndex).gameObject.SetActive(true);
    }
}
