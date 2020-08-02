using UnityEngine;

namespace App
{

	public interface IUIEssential
	{
		Camera UICamera { get; }
		Camera SystemUICamera { get; }
	}

}