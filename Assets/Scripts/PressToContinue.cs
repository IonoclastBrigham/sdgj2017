using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressToContinue : MonoBehaviour {

    private bool _pressed = false;

    public Sprite[] images;

    public UnityEngine.UI.Image ImagePanel;
    public int imageIndex =0;

    public SceneToLoad Scene;
	
    void Awake()
    {
        if (images != null && images.Length > 0)
            ImagePanel.sprite = images [0];
    }


	// Update is called once per frame
	void Update () {

        if (_pressed)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (images == null || images.Length == 0 || imageIndex >= (images.Length - 1))
            {
                _pressed = true;
                SceneController().LoadScene(Scene);
            } else
            {
                imageIndex++;
                ImagePanel.sprite = images [imageIndex];
            }
        }
    }

    private MasterSceneController SceneController()
    {
        return FindObjectOfType<MasterSceneController>();
    }
}
