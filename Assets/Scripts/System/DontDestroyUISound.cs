using UnityEngine;

public class DontDestroyUISound : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}