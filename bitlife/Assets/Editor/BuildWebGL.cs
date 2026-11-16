using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Headless WebGL build:
//   Unity -batchmode -nographics -projectPath <project> -executeMethod BuildWebGL.Build -quit
// Optional: -buildOutput <absolute-or-project-relative-output-dir>
public static class BuildWebGL
{
    public static void Build()
    {
        var output = Path.GetFullPath(Path.Combine(Application.dataPath, "../../webgl-build"));
        var args = Environment.GetCommandLineArgs();
        for (var i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "-buildOutput") output = Path.GetFullPath(args[i + 1]);
        }

        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
        PlayerSettings.WebGL.template = "APPLICATION:Default";
        PlayerSettings.WebGL.decompressionFallback = true;

        var options = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Scenes/Main.unity" },
            locationPathName = output,
            target = BuildTarget.WebGL,
            targetGroup = BuildTargetGroup.WebGL,
            options = BuildOptions.None,
        };
        var report = BuildPipeline.BuildPlayer(options);
        Debug.Log($"WebGL build result: {report.summary.result}, output: {output}");
        if (report.summary.result != BuildResult.Succeeded) EditorApplication.Exit(1);
    }
}
