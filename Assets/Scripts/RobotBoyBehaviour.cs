using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class RobotBoyBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private GameObject RestartButton;
    public void Kill()
    {
        GetComponent<Animator>().SetTrigger("Kill");
        Destroy(GetComponent<Platformer2DUserControl>());
        Destroy(GetComponent<PlatformerCharacter2D>());
        Destroy(GetComponentInChildren<Umbrella>());
        Instantiate(RestartButton);
    }
}
