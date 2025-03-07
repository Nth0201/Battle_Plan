using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    public Core core;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
       core = new Core();
    }
}
