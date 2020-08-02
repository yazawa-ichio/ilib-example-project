using ILib.Contents;
using UnityEngine;

namespace App
{
	public class Main : MonoBehaviour
	{
		void Start()
		{
			var obj = new GameObject(typeof(ContentsController).Name);
			GameObject.DontDestroyOnLoad(obj);
			var controller = obj.AddComponent<ContentsController>();
			BootParam param = new BootParam();
			param.BootContents.Add(SimpleParam.Create<SystemManager>());
			param.BootContents.Add(new GameManagerParam { BootScene = SimpleParam.Create<BootScene>() });
			param.ParallelBoot = false;
			controller.Boot(param);
		}
	}
}