namespace App.Debugs
{
	public interface IDebugManager
	{
#if DEBUG_BUILD
		void Bind<T>(T obj) where T : class;
		void Unbind<T>(T obj) where T : class;
		void Open();
		void Close();
		void ReOpen();
#endif
	}

}