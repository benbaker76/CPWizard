using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace CPWizard
{
    class CmdManager : IDisposable
    {
        private Mutex m_mutex = null;

        public CmdManager()
        {
        }

        public bool IsFirstInstance()
        {
            try
            {
                string m_UniqueIdentifier;
                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).CodeBase;
                m_UniqueIdentifier = assemblyName.Replace("\\", "_").ToLower();

                m_mutex = new Mutex(false, m_UniqueIdentifier);

                if (m_mutex.WaitOne(1, true)) // FirstInstance
                {
                    return true;
                }
                else
                {
                    m_mutex.Close();
                    m_mutex = null;

                    return false;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("IsFirstInstance", "CmdManager", ex.Message, ex.StackTrace);
            }

            return true;
        }

        public void OnChanged(string str)
        {
            try
            {
                string[] args = str.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                EventHandler.CmdArgsChanged(CmdLineToHash(args));
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("OnChanged", "CmdManager", ex.Message, ex.StackTrace);
            }
        }

        public void SetupCmdLineWatcher()
        {
            try
            {
                Global.InterCommManager.OnCmdArgsChanged += new InterCommManager.CmdArgsChangedHandler(OnChanged);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("SetupCmdLineWatcher", "CmdManager", ex.Message, ex.StackTrace);
            }
        }

        public void SetCmdLine(string[] args)
        {
            Global.InterCommManager.Data = String.Join("\n", args);
            Global.InterCommManager.DataMode = InterCommManager.DataModes.CmdLine;
        }

        public Dictionary<string, string> CmdLineToHash(string[] args)
        {
            try
            {
                if (args == null)
                    return null;

                Dictionary<string, string> argHash = new Dictionary<string, string>();

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        string Value = null;

                        if (i + 1 < args.Length)
                            Value = args[i + 1];

                        argHash.Add(args[i].ToLower(), Value);
                    }
                }

                return argHash;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CmdLineToHash", "CmdManager", ex.Message, ex.StackTrace);
            }

            return null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (m_mutex != null)
            {
                m_mutex.Close();
                m_mutex = null;
            }
        }

        #endregion
    }
}
