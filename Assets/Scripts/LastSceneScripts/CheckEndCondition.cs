using UnityEngine;

public class CheckEndCondition : MonoBehaviour
{
    public GameObject stairs;
    public GameObject Milo;
    public GameObject End;

    void Awake()
    {
        End.SetActive(false);
    }
    void Update()
    {
        if (stairs.activeSelf && !Milo.activeSelf)
        {
            End.SetActive(true);
        }
    }
}
