using ILib.AssetBundles;
using ILib.ServInject;
using UnityEngine;
using System;
using System.IO;
using Cysharp.Threading.Tasks;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace App.Assets
{
	public abstract class AssetBinder : MonoBehaviour
	{
		[SerializeField]
		protected string m_Target;

		public abstract Type GetAssetType();
	}

	public abstract class AssetBinder<T> : AssetBinder where T : UnityEngine.Object
	{
		[SerializeField]
		bool m_IgnoreError = false;

		public override Type GetAssetType()
		{
			return typeof(T);
		}

		void Awake()
		{
			if (!string.IsNullOrEmpty(m_Target))
			{
				Run().Forget();
			}
		}

		async UniTask Run()
		{
			while (!ABLoader.Initialized)
			{
				await UniTask.Yield();
			}
			var loader = ServInjector.Resolve<IResourceLoader>();
			var (bundle, asset) = ABLoader.GetReference(m_Target);
			var loading = loader.Load<T>(bundle, Path.GetFileNameWithoutExtension(asset));
			loading.IgnoreError = m_IgnoreError;
			OnLoad(await loading);
		}

		protected abstract void OnLoad(T asset);

	}


#if UNITY_EDITOR
	[CustomEditor(typeof(AssetBinder), true)]
	public class AssetBinderDrawer : Editor
	{
		AssetBinder m_Binder;

		private void OnEnable()
		{
			m_Binder = target as AssetBinder;
		}

		public override void OnInspectorGUI()
		{
			var obj = serializedObject;
			obj.UpdateIfRequiredOrScript();

			SerializedProperty property = obj.GetIterator();
			bool expanded = true;
			while (property.NextVisible(expanded))
			{
				if (property.propertyPath == "m_Target")
				{
					DrawSelect(property);
				}
				else
				{
					using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
					{
						EditorGUILayout.PropertyField(property, true);
					}
				}
				expanded = false;
			}
			obj.ApplyModifiedProperties();
		}

		void DrawSelect(SerializedProperty property)
		{
			var path = AssetDatabase.GUIDToAssetPath(property.stringValue);
			var current = AssetDatabase.LoadAssetAtPath(path, m_Binder.GetAssetType());
			var ret = EditorGUILayout.ObjectField("Target", current, m_Binder.GetAssetType(), allowSceneObjects: false);
			if (ret != current)
			{
				if (ret == null)
				{
					property.stringValue = "";
				}
				else
				{
					path = AssetDatabase.GetAssetPath(ret);
					property.stringValue = AssetDatabase.AssetPathToGUID(path);
				}
			}
		}

	}
#endif

}