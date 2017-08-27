using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ScreenShaker : MonoBehaviour {

    private bool _shakin;
    private Camera _camera;
    private Vector3 _startPos;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        _startPos = _camera.transform.position;
    }

    public void StartShakin()
    {
        _shakin = true;
    }

    public void StopShakin()
    {
        _shakin = false;
    }

    void Update()
    {
        if (_shakin)
        {
            var offset = 0.3f * Random.insideUnitSphere;
            offset.y = 0;
            _camera.transform.position = _startPos + offset;
        } else
        {
            _camera.transform.position = _startPos;
        }
    }
}
