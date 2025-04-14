using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public static Flag Instance {get; private set;}
    
    public GameObject holder = null;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("character") && holder == null)
        {
            AudioManager.Instance.Flag.Play();
            holder = collider.gameObject;
            transform.SetParent(collider.gameObject.transform.Find("FlagRoot"));

            if (collider.gameObject == Enemy.Instance.gameObject)
            {
                Player.Instance.transform.Find("Obstacle").gameObject.SetActive(true);
            }
        }
    }
}