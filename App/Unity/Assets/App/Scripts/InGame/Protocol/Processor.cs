using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace App.InGame.Protocol
{
	public static class Processor
	{

		public static byte[] Pack(object obj)
		{
			return s_Serialize[obj.GetType()](obj);
		}

		public static object Unpack(byte[] buf)
		{
			return s_Deserialize[buf[0]](buf);
		}

		static Dictionary<Type, Func<object, byte[]>> s_Serialize = new Dictionary<Type, Func<object, byte[]>>();
		static Dictionary<byte, Func<byte[], object>> s_Deserialize = new Dictionary<byte, Func<byte[], object>>();

		static Processor()
		{
			// Room管理用
			Register<Ready>(1);
			Register<Result>(2);

			// Game用
			Register<SyncRacket>(100);
			Register<SyncBall>(101);
		}

		static void Register<T>(byte id)
		{
			s_Serialize.Add(typeof(T), (obj) =>
			{
				var json = JsonUtility.ToJson(obj);
				var count = Encoding.UTF8.GetByteCount(json);
				var buf = new byte[count + 1];
				buf[0] = id;
				Encoding.UTF8.GetBytes(json, 0, json.Length, buf, 1);
				return buf;
			});
			s_Deserialize.Add(id, (buf) =>
			{
				var json = Encoding.UTF8.GetString(buf, 1, buf.Length - 1);
				return JsonUtility.FromJson<T>(json);
			});
		}


	}
}