namespace T4SQL.EngineService
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.templateEngineServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.templateEngineServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// templateEngineServiceProcessInstaller
			// 
			this.templateEngineServiceProcessInstaller.Password = null;
			this.templateEngineServiceProcessInstaller.Username = null;
			// 
			// templateEngineServiceInstaller
			// 
			this.templateEngineServiceInstaller.Description = "Build SQL object code of requested workitems based on T4 Templates";
			this.templateEngineServiceInstaller.DisplayName = "T4SQL Template Engine Service";
			this.templateEngineServiceInstaller.ServiceName = "T4SQL Template Engine";
			this.templateEngineServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.templateEngineServiceInstaller_AfterInstall);
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.templateEngineServiceProcessInstaller,
            this.templateEngineServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller templateEngineServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller templateEngineServiceInstaller;
	}
}