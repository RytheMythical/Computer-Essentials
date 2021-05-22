using System;
using System.Collections.Generic;
using System.Text;

namespace File_Vault_API
{
    public class FileVaultEvents : File_Vault
    {
        public delegate void VaultCreatedDelegate(object source, FileVaultCompletedClass e);

        public class FileVaultCompletedClass : EventArgs
        {
            private string StoreText = "";
            public FileVaultCompletedClass(string Text)
            {
                StoreText = Text;
            }

            public string GetArgs
            {
                get
                {
                    return StoreText;
                }
            }
        }

        public delegate void CreatingVaultStartedDelegate(object source, CreatingVaultStartedClass e);

        public class CreatingVaultStartedClass : EventArgs
        {
            private string StoreText = "";

            public CreatingVaultStartedClass(string Text)
            {
                StoreText = Text;
            }

            public string GetText()
            {
                return StoreText;
            }
        }

        public class OpeningVaultStartedClass : EventArgs
        {
            private string StoreText = "";

            public OpeningVaultStartedClass(string Text)
            {
                StoreText = Text;
            }

            public string GetText()
            {
                return StoreText;
            }
        }

        public delegate void OpeningVaultStartedDelegate(object source, OpeningVaultStartedClass e);

        public class OpenedVaultClass : EventArgs
        {
            private string StoreText = "";

            public OpenedVaultClass(string Text)
            {
                StoreText = Text;
            }

            public string GetText()
            {
                return StoreText;
            }
        }

        public delegate void OpenedVaultDelegate(object source, OpenedVaultClass e);

        public class ClosingVaultClass : EventArgs
        {
            private string StoreText = "";

            public ClosingVaultClass(string Text)
            {
                StoreText = Text;
            }

            public string GetText()
            {
                return StoreText;
            }
        }

        public delegate void ClosingVaultDelegate(object source, ClosingVaultClass e);

        public class ClosedVaultClass : EventArgs
        {
            private string StoreText = "";

            public ClosedVaultClass(string Text)
            {
                StoreText = Text;
            }

            public string GetText()
            {
                return StoreText;
            }
        }

        public delegate void ClosedVaultDelegate(object source, ClosedVaultClass e);
    }
}
