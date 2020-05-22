using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * AntiPiracy.cs - Permits the game only to run on allowed hosts
 * Copyright (C) 2010 Justin Lloyd
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU lesser General Public License
 * along with this library.  If not, see <http://www.gnu.org/licenses/>.
 *
 * Script modified by Felipe Nunes <https://nyune.su/>
 *
 */

namespace mellofnd
{
	public class SiteLocker : MonoBehaviour
	{
		public string m_bounceToURL = "https://nyune.su/games/";

		public bool m_permitLocalHost;

		public string[] m_permittedLocalHosts;

		public string[] m_permittedRemoteHosts =
		{
			"coolmathgames.com", "coolmath.com", "coolmath4kids.com", "addictinggames.com", "shockwave.com",
			"armorgames.com", "kongregate.com", "konggames.com", "newgrounds.com", "ungrounded.net", "nyunesu.itch.io",
			"itch.zone", "v6p9d9t4.ssl.hwcdn.net", "github.io", "nyune.su", "nyunejam.itch.io", "github.com"
		};

		private void Reset()
		{
			m_permitLocalHost = true;
			m_permittedLocalHosts = new[]
				{"file://", "http://localhost/", "http://localhost:", "https://localhost/", "https://localhost:"};
		}

		private void Start()
		{
			if (!Application.isEditor)
				PiracyCheck();
		}

		private static bool IsValidHost(string[] hosts)
		{
			if (!Debug.isDebugBuild) return hosts.Any(host => Application.absoluteURL.ToLower().Contains(host));
			var msg = new StringBuilder();
			msg.Append("Checking against list of hosts: ");
			foreach (var url in hosts)
			{
				msg.Append(url.ToLower());
				msg.Append(",");
			}

			Debug.Log(msg.ToString());

			return hosts.Any(host => Application.absoluteURL.ToLower().Contains(host));
		}

		private bool IsValidLocalHost()
		{
			return m_permitLocalHost && IsValidHost(m_permittedLocalHosts);
		}

		private bool IsValidRemoteHost()
		{
			return IsValidHost(m_permittedRemoteHosts);
		}

		private void Bounce()
		{
			Application.OpenURL(m_bounceToURL);
		}

		private bool IsValidHost()
		{
			return IsValidLocalHost() || IsValidRemoteHost();
		}

		private static string CompileHosts(IReadOnlyList<string> permittedHosts)
		{
			var hosts = new StringBuilder();

			for (var i = 0; i < permittedHosts.Count; i++)
			{
				hosts.Append("(document.location.host.includes('");
				var url = permittedHosts[i];
				if (url.IndexOf("http://", StringComparison.Ordinal) == 0)
					url = url.Substring(7);
				else if (url.IndexOf("https://", StringComparison.Ordinal) == 0) url = url.Substring(8);

				hosts.Append(url);
				hosts.Append("'))");
				if (i < permittedHosts.Count - 1) hosts.Append(" && ");
			}

			return hosts.ToString();
		}

		private void CheckWithJavaScript()
		{
			var javascriptTest = new StringBuilder();

			javascriptTest.Append("if (");
			if (m_permitLocalHost)
			{
				javascriptTest.Append("(document.location.host != 'localhost') && (document.location.host != '')");
				if (m_permittedRemoteHosts.Length > 0) javascriptTest.Append(" && ");
			}

			javascriptTest.Append(CompileHosts(m_permittedRemoteHosts));
			javascriptTest.Append("){ document.location='");
			javascriptTest.Append(m_bounceToURL);
			javascriptTest.Append("'; }");
			if (Debug.isDebugBuild) Debug.Log(javascriptTest);

#pragma warning disable 618
			Application.ExternalEval(javascriptTest.ToString());
#pragma warning restore 618
		}

		private void PiracyCheck()
		{
			if (Debug.isDebugBuild)
				Debug.Log($"The absolute URL of the application is {Application.absoluteURL}");

			if (IsValidHost() == false)
			{
				if (Debug.isDebugBuild)
					Debug.Log($"Failed valid remote host test. Bouncing player to {m_bounceToURL}");

				Bounce();
				return;
			}

			CheckWithJavaScript();
		}
	}
}