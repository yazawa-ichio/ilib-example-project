using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExBuild = ILib.AssetBundles.ExManifest.Tools.Build;
namespace App.Tools
{
	using static Util;

	public static class Build
	{
		[MenuItem("Tools/Build/AssetBundle")]
		public static void BuildBundle()
		{
			ExBuild.DefaultBuid(EditorUserBuildSettings.activeBuildTarget);
		}

		[MenuItem("Tools/Build/AssetBundle Copy To StreamingAssets")]
		public static void BuildBundleAndCopyStreaming()
		{
			BuildBundleAndCopyStreaming(EditorUserBuildSettings.activeBuildTarget);
		}

		public static void BuildBundleAndCopyStreaming(BuildTarget target)
		{
			ExBuild.DefaultBuid(target);
			var source = Application.dataPath.Replace("Assets", "AssetBundles/" + target.ToString());
			DirectoryCopy(source, Application.streamingAssetsPath + "/assets");
		}

	}
}