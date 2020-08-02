using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
	public class RefCountAction : RefCountActionBase
	{
		public Action OnRefAction { get; set; }
		public Action OnRemoveRefAction { get; set; }

		protected override void OnRef()
		{
			OnRefAction?.Invoke();
		}

		protected override void OnRemoveRef()
		{
			OnRemoveRefAction?.Invoke();
		}
	}

	public class RefCountToggle : RefCountActionBase
	{
		public Action<bool> OnChange { get; set; }
		public bool Value { get; private set; }

		protected override void OnRef()
		{
			Value = true;
			OnChange?.Invoke(true);
		}

		protected override void OnRemoveRef()
		{
			Value = false;
			OnChange?.Invoke(false);
		}
	}

	public abstract class RefCountActionBase : System.IDisposable
	{
		public class Ref : IDisposable
		{
			RefCountActionBase m_Parent;

			public Ref(RefCountActionBase parent)
			{
				m_Parent = parent;
				m_Parent.AddRef();
			}

			~Ref() => Dispose();

			public void Dispose()
			{
				m_Parent.RemoveRef();
				GC.SuppressFinalize(this);
			}
		}

		int m_RefCount = 0;
		object m_Lock = new object();
		List<RefCountActionBase> m_Depends;
		bool m_Disposed = false;

		public void Dispose()
		{
			m_Disposed = true;
		}

		public void DependTo(RefCountActionBase target)
		{
			if (m_Depends == null) m_Depends = new List<RefCountActionBase>();
			m_Depends.Add(target);
		}

		protected abstract void OnRef();
		protected abstract void OnRemoveRef();

		public IDisposable CrateRefHandle()
		{
			lock (m_Lock)
			{
				return new Ref(this);
			}
		}

		public void AddRef()
		{
			if (m_Disposed) return;
			lock (m_Lock)
			{
				m_RefCount++;
				if (m_RefCount == 1)
				{
					OnRef();
				}
				if (m_Depends != null)
				{
					foreach (var dep in m_Depends) dep.AddRef();
				}
			}
		}

		public void RemoveRef()
		{
			if (m_Disposed) return;
			lock (m_Lock)
			{
				m_RefCount--;
				Debug.Assert(m_RefCount >= 0, "参照カウントがマイナスになりました");
				if (m_RefCount == 0)
				{
					OnRemoveRef();
				}
				if (m_Depends != null)
				{
					foreach (var dep in m_Depends) dep.RemoveRef();
				}
			}
		}
	}
}