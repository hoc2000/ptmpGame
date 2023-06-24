using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using My.Tool;
//using Com.LuisPedroFonseca.ProCamera2D;

public class MyTools
{

    [MenuItem("MyTools/Clear Json")]
    private static void DeletaJson()
    {
        SaveGameManager.I.DeleteAll();
    }

    [MenuItem("MyTools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    
    [MenuItem("MyTools/Screen Shot Without Canvas &z")]
    private static void GetScrShot()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        canvas?.gameObject.SetActive(false);

        // File path
        string folderPath = "D:/screenshots/";
        string fileName = "scr";

        // Create the folder beforehand if not exists
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
        int i = 0;
        while (System.IO.File.Exists(folderPath + fileName + ".png"))
        {
            fileName = "scr" + i;
            i++;
        }
        Debug.Log(folderPath + fileName + ".png");
        // Capture and store the screenshot
        ScreenCapture.CaptureScreenshot(folderPath + fileName + ".png");
    }


    [MenuItem("MyTools/Screen Shot With Canvas &a")]
    private static void GetScrShotCanvas()
    {
       

        // File path
        string folderPath = "D:/screenshots/";
        string fileName = "scr";

        // Create the folder beforehand if not exists
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
        int i = 0;
        while (System.IO.File.Exists(folderPath + fileName + ".png"))
        {
            fileName = "scr" + i;
            i++;
        }
        Debug.Log(folderPath + fileName + ".png");
        // Capture and store the screenshot
        ScreenCapture.CaptureScreenshot(folderPath + fileName + ".png");
    }


    //[MenuItem("MyTools/UnLightStatic &r")]
    //private static void UnLightStatic()
    //{
    //    foreach (var item in EditorBuildSettings.scenes)
    //    {

    //        EditorSceneManager.OpenScene(item.path);

    //        var meshes = GameObject.FindObjectsOfType<MeshRenderer>();
    //        foreach (var itemMesh in meshes)
    //        {
    //            if(itemMesh.GetComponent<Game2DWaterKit.Game2DWater>()!=null)
    //            {
    //                itemMesh.gameObject.name = "Water";
    //                itemMesh.gameObject.isStatic = false;
    //            }
    //            else
    //            {
    //                Debug.Log(item.path + "    " + itemMesh.name);
    //                //  itemMesh.gameObject.isStatic = false;
    //                itemMesh.gameObject.name = "Terrain";
    //                GameObjectUtility.SetStaticEditorFlags(itemMesh.gameObject, StaticEditorFlags.BatchingStatic);
    //            }
             
    //        }
    //        Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));
 
    //    }
    //}


    //[MenuItem("MyTools/Update Star")]
    //private static void UpdateStar()
    //{
    //    foreach (var item in EditorBuildSettings.scenes)
    //    {

    //        EditorSceneManager.OpenScene(item.path);

    //        var setupLevel = GameObject.FindObjectOfType<SetupLevel>();
    //        if (setupLevel == null)
    //            continue;
    //        setupLevel.CreateStarID();
    //        Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

    //    }
    //}
    //[MenuItem("MyTools/Update Coin")]
    //private static void UpdateCoin()
    //{
    //    foreach (var item in EditorBuildSettings.scenes)
    //    {

    //        EditorSceneManager.OpenScene(item.path);

    //        var setupLevel = GameObject.FindObjectOfType<SetupLevel>();
    //        if (setupLevel == null)
    //            continue;
    //        setupLevel.CreateStarCoin();
    //        Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

    //    }
    //}
    [MenuItem("MyTools/Open spl &1")]
    private static void OpenSpl()
    {
        foreach (var item in EditorBuildSettings.scenes)
        {
            if(item.path.Contains("SplashScreen"))
            EditorSceneManager.OpenScene(item.path);
        
        }
    }
    //[MenuItem("MyTools/Update Bound")]
    //private static void UpdateBound()
    //{
    //    foreach (var item in EditorBuildSettings.scenes)
    //    {

    //        EditorSceneManager.OpenScene(item.path);

    //        var proCamera2DNumericBoundaries = GameObject.FindObjectOfType<ProCamera2DNumericBoundaries>();
    //        if (proCamera2DNumericBoundaries == null)
    //            continue;
    //        proCamera2DNumericBoundaries.UseBottomBoundary = false;
    //        Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

    //    }
    //}

    [MenuItem("MyTools/ Create ContentMgr")]
    private static void CreateContentMgr()
    {
        foreach (var item in EditorBuildSettings.scenes)
        {

            EditorSceneManager.OpenScene(item.path);

            var contentMgrs = GameObject.FindObjectsOfType<ContentMgr>();

            foreach (var itemContent in contentMgrs)
            {
                if (itemContent.gameObject.name.Contains("ContentMgr"))
                    UnityEngine.Object.DestroyImmediate(itemContent.gameObject);
                else

                UnityEngine.Object.DestroyImmediate(itemContent);
            }

            var level = GameObject.Find("Level");
            if(level!=null)
            {
                GameObject.Instantiate(Resources.Load("ContentMgr"), level.transform);
            }
         
            Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

        }
    }


    [MenuItem("MyTools/ Create Door")]
    private static void CreateDoor()
    {
        foreach (var item in EditorBuildSettings.scenes)
        {

            EditorSceneManager.OpenScene(item.path);

            //var contentMgrs = GameObject.FindObjectsOfType<ContentMgr>();

            //foreach (var itemContent in contentMgrs)
            //{
            //    if (itemContent.gameObject.name.Contains("ContentMgr"))
            //        UnityEngine.Object.DestroyImmediate(itemContent.gameObject);
            //    else

            //        UnityEngine.Object.DestroyImmediate(itemContent);
            //}

            var level = GameObject.Find("Level");
            if (level != null)
            {
                GameObject.Instantiate(Resources.Load("Door"), level.transform);
            }

            Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

        }
    }

    [MenuItem("MyTools/ Remove Fader")]
    private static void RemoveFader()
    {
        foreach (var item in EditorBuildSettings.scenes)
        {

            EditorSceneManager.OpenScene(item.path);

            //var contentMgrs = GameObject.FindObjectsOfType<ContentMgr>();

            //foreach (var itemContent in contentMgrs)
            //{
            //    if (itemContent.gameObject.name.Contains("ContentMgr"))
            //        UnityEngine.Object.DestroyImmediate(itemContent.gameObject);
            //    else

            //        UnityEngine.Object.DestroyImmediate(itemContent);
            //}

            var level = GameObject.Find("Fader");
            if (level != null)
            {
                GameObject.DestroyImmediate(level);
            }

            Debug.Log(EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene()));

        }
    }


}
