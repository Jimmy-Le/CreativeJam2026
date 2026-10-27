#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class LevelJsonImporterWindow : EditorWindow
{
    [MenuItem("Tools/Import Levels From JSON")]
    public static void ShowWindow()
    {
        GetWindow<LevelJsonImporterWindow>("Level JSON Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Batch Import Levels", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox("This will scan 'Assets/Resources/Levels' for JSON files and create or update Level ScriptableObjects in 'Assets/ScriptableObjects/Levels'.", MessageType.Info);
        
        EditorGUILayout.Space();

        if (GUILayout.Button("Generate ScriptableObjects", GUILayout.Height(30)))
        {
            ImportLevels();
        }
    }

    private static void ImportLevels()
    {
        string jsonFolderPath = Path.Combine(Application.dataPath, "Resources/Levels");

        if (!Directory.Exists(jsonFolderPath))
        {
            EditorUtility.DisplayDialog("Directory Not Found", $"Could not find folder at:\n{jsonFolderPath}", "OK");
            return;
        }

        string[] jsonFiles = Directory.GetFiles(jsonFolderPath, "*.json");

        if (jsonFiles.Length == 0)
        {
            EditorUtility.DisplayDialog("No Files Found", "No .json files were found in the Resources/Levels folder.", "OK");
            return;
        }

        string outputFolder = "Assets/ScriptableObjects/Levels";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            AssetDatabase.Refresh();
        }

        int successCount = 0;

        foreach (string filePath in jsonFiles)
        {
            string jsonText = File.ReadAllText(filePath);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string assetPath = $"{outputFolder}/{fileName}.asset";

            Level levelAsset = AssetDatabase.LoadAssetAtPath<Level>(assetPath);
            bool isNewAsset = false;

            if (levelAsset == null)
            {
                levelAsset = ScriptableObject.CreateInstance<Level>();
                isNewAsset = true;
            }

            levelAsset.LoadFromJson(jsonText);

            if (isNewAsset)
            {
                AssetDatabase.CreateAsset(levelAsset, assetPath);
                Debug.Log($"Created new Level asset: {assetPath}");
            }
            else
            {
                EditorUtility.SetDirty(levelAsset);
                Debug.Log($"Updated existing Level asset: {assetPath}");
            }

            successCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        EditorUtility.DisplayDialog("Import Complete", $"Successfully processed {successCount} level JSON files!", "OK");
    }
}
#endif
