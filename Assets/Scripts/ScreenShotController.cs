using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ScreenShotController : MonoBehaviour
{
    public KeyCode m_ScreenShotButton = KeyCode.F9;
    const string m_DefaultDir         = "Screenshots";
    string       m_FilePath           = null;

    public void TriggerCapture( string file_path )
    {
        m_FilePath = file_path;
        StartCoroutine( SaveScreenAsPNG() );
    }

    void Update()
    {
        if ( Input.GetKeyDown( m_ScreenShotButton ) )
        {
            m_FilePath = null;

            StartCoroutine( SaveScreenAsPNG() );
        }
    }

    // https://docs.unity3d.com/ScriptReference/WWWForm.html
    IEnumerator SaveScreenAsPNG()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D( width, height, TextureFormat.RGB24, false );

        tex.ReadPixels( new Rect(0, 0, width, height), 0, 0 );
        tex.Apply();

        byte[] bytes = tex.EncodeToPNG();

        string file_path = m_FilePath;
        if ( m_FilePath == null )
        {
            file_path = GetScreenShotFilePath();
            TryCreateScreenShotDir();
        }

        File.WriteAllBytes( file_path, bytes );

        Destroy( tex );
    }

    public void TryCreateScreenShotDir()
    {
        if ( ! Directory.Exists( m_DefaultDir ) )
        {
            Directory.CreateDirectory( m_DefaultDir );
        }
    }

    public string GetScreenShotFilePath()
    {
        System.DateTime this_day = System.DateTime.Now;
        string time_str = 
            string.Format( "{0}{1}{2}_{3}_{4}_{5}",
	                        this_day.Year, this_day.Month, this_day.Day,
                            this_day.Hour, this_day.Minute, this_day.Second );
        string filename = string.Format( "{0}/Screenshot_{1}.png", m_DefaultDir, time_str );

        return filename;
    }
}