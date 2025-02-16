using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class TagUpdater
{
    private const string TAG_DATABASE_PATH = "Assets/Project/DataConfig/Utilities/TagDataBase.asset"; 
    private const string ENUM_FILE_PATH = "Assets/Project/Scripts/Utilities/TagEnum.cs"; 

    static TagUpdater()
    {
        EditorApplication.update += CheckAndUpdateTags;
    }

    private static void CheckAndUpdateTags()
    {
        TagDataBase tagDatabase = AssetDatabase.LoadAssetAtPath<TagDataBase>(TAG_DATABASE_PATH);
        if (tagDatabase == null) return;
        string[] unityTags = UnityEditorInternal.InternalEditorUtility.tags;
        bool updated = false;
        foreach (string tag in unityTags)
        {
            if (tagDatabase.strTags.Contains(tag)) continue;
            tagDatabase.strTags.Add(tag);
            updated = true;
        }

        if (!updated) return;
        EditorUtility.SetDirty(tagDatabase);
        AssetDatabase.SaveAssets();
        Debug.Log("Tag SO updated.");
        GenerateTagEnum(tagDatabase.strTags);
    }

    private static void GenerateTagEnum(List<string> tags)
    {
        using (StreamWriter writer = new StreamWriter(ENUM_FILE_PATH))
        {
            writer.WriteLine("// Auto-generated Tag Enum");
            writer.WriteLine("public enum EGameTags");
            writer.WriteLine("{");
            for (int i = 0; i < tags.Count; i++)
            {
                string safetag = tags[i].Replace(" ", "");
                safetag = string.Format($"\t{safetag},");
                writer.WriteLine(safetag);
            }
            writer.WriteLine("}");
        }

        AssetDatabase.Refresh();
        Debug.Log("GameTags Enum updated");
    }
}

#endif


