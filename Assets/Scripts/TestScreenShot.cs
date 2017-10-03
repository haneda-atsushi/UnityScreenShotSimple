using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TestScreenShot : MonoBehaviour
{
    ScreenShotController m_ScreenShotController;

	void Start ()
    {
        m_ScreenShotController = FindObjectOfType<ScreenShotController>();
        Assert.IsNotNull( m_ScreenShotController );

        m_ScreenShotController.TryCreateScreenShotDir();
    }

    void OnGUI()
    {
        if ( GUI.Button( new Rect( 100, 100, 200, 100 ), "Take screenshot" ) )
        {
            string file_path = m_ScreenShotController.GetScreenShotFilePath();
            m_ScreenShotController.TriggerCapture( file_path );

            Debug.LogFormat( "Screenshot is saved to {0}.", file_path );
        }
    }
}
