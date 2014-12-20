using AIGames.BotDeployment;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIGames.Warlight2.UnitTests.Deployment
{
	[TestFixture]
	public class DeployTest
	{
#if DEBUG
		private const bool IsDebugDeploy = true;
#else
		private const bool IsDebugDeploy = false;
#endif

		//[Test]
		//public void Deploy_Bot_Successful()
		//{
		//	var collectDir = new DirectoryInfo(Path.Combine(AppDir.FullName, "AIGames.Bot.Namespace"));
		//	Deployer.Run(collectDir, "BotName", "Version", IsDebugDeploy);
		//}

		public static DirectoryInfo AppDir
		{
			get
			{
				return new DirectoryInfo(ConfigurationManager.AppSettings["App.Dir"]);
			}
		}
	}
}
