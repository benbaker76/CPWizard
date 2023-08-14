using System;
using System.Collections.Generic;
using System.Text;

namespace CPWizard
{
	partial class frmOptions
	{
		private void GetIRCOptions()
		{
			txtIRCServer.Text = Settings.IRC.Server;
			txtIRCPort.Text = StringTools.ToString<int>(Settings.IRC.Port);
			txtIRCChannel.Text = Settings.IRC.Channel;
			txtIRCNickName.Text = Settings.IRC.NickName;
			txtIRCUserName.Text = Settings.IRC.UserName;
			txtIRCRealName.Text = Settings.IRC.RealName;
			chkIRCIsInvisible.Checked = Settings.IRC.IsInvisible;
		}

		private void SetIRCOptions()
		{
			Settings.IRC.Server = txtIRCServer.Text;
			Settings.IRC.Port = StringTools.FromString<int>(txtIRCPort.Text);
			Settings.IRC.Channel = txtIRCChannel.Text;
			Settings.IRC.NickName = txtIRCNickName.Text;
			Settings.IRC.UserName = txtIRCUserName.Text;
			Settings.IRC.RealName = txtIRCRealName.Text;
			Settings.IRC.IsInvisible = chkIRCIsInvisible.Checked;
		}
	}
}
