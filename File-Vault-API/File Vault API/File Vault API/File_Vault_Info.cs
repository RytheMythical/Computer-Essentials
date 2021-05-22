using System;
using System.Collections.Generic;
using System.Text;

namespace File_Vault_API
{
    public class File_Vault_Info
    {
        private VaultInfoClass VaultInfoClassSet;
        public VaultInfoClass VaultInfo
        {
            get
            {
                if (VaultInfoClassSet == null)
                {
                    VaultInfoClassSet = new VaultInfoClass();
                }
                return VaultInfoClassSet;
            }
        }
    }

    public class VaultInfoClass
    {
        /// <summary>
        /// Sets the vault path (unlock and lock)
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Sets the Password (unlock and lock)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Sets the PIM (unlock and lock)
        /// </summary>
        public int PIM {
            get
            {
                if (PIMSet == 0)
                {
                    PIMSet = 500;
                }
                return PIMSet;
            }
            set
            {
                PIMSet = value;
            }}

            private int PIMSet = 0;

        /// <summary>
        /// Sets a custom drive letter, if you don't, a random one will be generated, and can be accessed
        /// by using File_Vault.DriveLetter
        /// </summary>
        public string CustomDriveLetter { get; set; }

        /// <summary>
        /// Sets the size of the file vault (create only)
        /// </summary>
        public int Size { get; set; }
    }
}
