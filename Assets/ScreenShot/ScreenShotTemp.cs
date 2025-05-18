using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotTemp : MonoBehaviour
{
    public bool takeScreenShot;

    private Camera cam;

    private int resWid;
    private int resHei;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam.targetTexture == null)
        {
            cam.targetTexture = new RenderTexture(resWid, resHei, 24);
        }
        else
        {
            resWid = cam.targetTexture.width;
            resHei = cam.targetTexture.height;
        }
    }

    private void Update()
    {
        if (takeScreenShot)
        {
            TakeScreenShot();
            takeScreenShot = false;
        }
    }

    private void TakeScreenShot()
    {
        if (cam.gameObject.activeInHierarchy)
        {
            Texture2D snapshot = new Texture2D(resWid, resHei, TextureFormat.ARGB32, false);
            cam.Render();
            RenderTexture.active = cam.targetTexture;
            snapshot.ReadPixels(new Rect(0, 0, resWid, resHei), 0, 0);
            byte[] bytes = snapshot.EncodeToPNG();
            string fileName = SnapshotName();
            System.IO.File.WriteAllBytes(fileName, bytes);
            print("Screenshot taken");
        }
        print("l");
    }

    private string SnapshotName()
    {
        return string.Format("{0}/Screenshot/snap_{1}x{2}_{3}.png",
            Application.dataPath,
            resWid,
            resHei,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}
