using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.UI;

public class HiResScreenShots : MonoBehaviour
{
    public Camera camera;
    public int resWidth = 300;
    public int resHeight = 300;

    private bool takeHiResShot = true;

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
            Application.dataPath,
            width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));
    }

    public void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    async void FixedUpdate()
    {
        //if (Input.GetKeyDown("k"))
        {
            Debug.Log("Start ScreenShot!!");
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.ARGB32, false);
            camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(resWidth, resHeight);
            await Task.Run(() => ScreenShot(filename, bytes));

            //Texture2D t;
            //t.LoadImage(bytes);
        }
    }

    void ScreenShot(string filename, byte[] bytes)
    {
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
        takeHiResShot = false;
        Debug.Log("Capture!!");
    }
}
