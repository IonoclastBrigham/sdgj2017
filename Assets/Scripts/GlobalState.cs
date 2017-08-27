using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour {

    public int FirstSelectedWireIndex = -1;
    public int CorrectWire = -1;

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }
}
