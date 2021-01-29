using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundLibrary))]
public class SoundLibraryEditor : Editor
{
    private SoundLibrary _target;

    private void OnEnable()
    {
        _target = (SoundLibrary) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(10);
        
        if (GUILayout.Button("Update Entity types"))
        {
            // if(_target.blockTypes.Count>0)
            UpdateBlockTypes(Application.dataPath + "/Scripts/Utils/SoundItem.cs");
        }
    }

    private void UpdateBlockTypes(string filePath)
    {
        string types = string.Empty;
        
        foreach (var block in _target.soundItems)
        {
            types += 
                "\t"+"\t" + block.ToUpper() + ",\n";
        }

        types = types.Substring(0, types.Length - 2);

        string content =
            "namespace Utils" + "\n" +
            "{ " + "\n" +
            "\t" + "public enum SoundItem " + "\n" +
            "\t" + '{' + "\n" +
            types + "\n" +
            "\t"+'}' + "\n" +
            '}';

        FileStream fileStream = new FileStream(            
            filePath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None);

        using (TextWriter textWriter = new StreamWriter(fileStream))
        {
            textWriter.Write(content);
        }
        fileStream.Close();
        
        AssetDatabase.Refresh();
    }

}