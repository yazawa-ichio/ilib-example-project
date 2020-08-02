using System.IO;

namespace App.Tools
{
	public static class Util
	{
		public static void DirectoryCopy(string sourcePath, string destinationPath)
		{
			DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
			DirectoryInfo destinationDirectory = new DirectoryInfo(destinationPath);

			//コピー先のディレクトリがなければ作成する
			if (destinationDirectory.Exists == false)
			{
				destinationDirectory.Create();
				destinationDirectory.Attributes = sourceDirectory.Attributes;
			}

			//ファイルのコピー
			foreach (FileInfo fileInfo in sourceDirectory.GetFiles())
			{
				if (fileInfo.Extension != ".bundle" && fileInfo.Name != "manifest") continue;
				//同じファイルが存在していたら、常に上書きする
				fileInfo.CopyTo(destinationDirectory.FullName + @"/" + fileInfo.Name, true);
			}

			//ディレクトリのコピー（再帰を使用）
			foreach (DirectoryInfo directoryInfo in sourceDirectory.GetDirectories())
			{
				DirectoryCopy(directoryInfo.FullName, destinationDirectory.FullName + @"/" + directoryInfo.Name);
			}
		}
	}
}