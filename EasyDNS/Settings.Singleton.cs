using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace theDiary.EasyDNS.Windows
{
    public partial class Settings
    {
        #region Private Static Declarations
        private static readonly object syncObject = new object();
        private static Settings instance;
        #endregion

        #region Private Static Properties
        /// <summary>
        /// Get the Application Guid
        /// </summary>
        private static Guid AppGuid
        {
            get
            {
                Assembly asm = Assembly.GetEntryAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }

        /// <summary>
        /// Get the current assembly Guid.
        /// <remarks>
        /// Note that the Assembly Guid is not necessarily the same as the
        /// Application Guid - if this code is in a DLL, the Assembly Guid
        /// will be the Guid for the DLL, not the active EXE file.
        /// </remarks>
        /// </summary>
        private static Guid AssemblyGuid
        {
            get
            {
                Assembly asm = Assembly.GetExecutingAssembly();
                object[] attr = (asm.GetCustomAttributes(typeof(GuidAttribute), true));
                return new Guid((attr[0] as GuidAttribute).Value);
            }
        }

        /// <summary>
        /// Get the current user data folder
        /// </summary>
        private static string UserDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }

        /// <summary>
        /// Get the current user roaming data folder
        /// </summary>
        private static string UserRoamingDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }

        /// <summary>
        /// Get all users data folder
        /// </summary>
        private static string AllUsersDataFolder
        {
            get
            {
                Guid appGuid = AppGuid;
                string folderBase = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string dir = string.Format(@"{0}\{1}\", folderBase, appGuid.ToString("B").ToUpper());
                return CheckDir(dir);
            }
        }
        #endregion

        #region Public Static Properties
        public static Settings Instance
        {
            get
            {
                lock (Settings.syncObject)
                {
                    if (Settings.instance == null)
                        Load();

                    return Settings.instance;
                }
            }
        }
        #endregion

        #region Methods & Functions
        public static void Save()
        {
            lock (Settings.syncObject)
            {
                try
                {
                    var filePath = Path.Combine(Settings.UserDataFolder, "EasyDNS.settings");
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    using (var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        formatter.Serialize(stream, Settings.instance);
                        stream.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private static void Load()
        {
            lock (Settings.syncObject)
            {
                var filePath = Path.Combine(Settings.UserDataFolder, "EasyDNS.settings");
                using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    try
                    {
                        if (stream.Length == 0)
                            return;

                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        Settings.instance = (Settings)formatter.Deserialize(stream);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    finally
                    {
                        if (Settings.instance == null)
                            Settings.instance = new Settings();
                    }
                }
            }
        }


        /// <summary>
        /// Check the specified folder, and create if it doesn't exist.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static string CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return dir;
        }
        #endregion
    }
}
