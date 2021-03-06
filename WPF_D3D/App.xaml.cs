using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace IE_WPF_D3D
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static NVOptimusEnabler _enabler;

        protected override void OnStartup(StartupEventArgs e)
        {
            _enabler = new NVOptimusEnabler();
            base.OnStartup(e);
        }

        private sealed class NVOptimusEnabler
        {
            static NVOptimusEnabler()
            {
                try
                {

                    if (Environment.Is64BitProcess)
                        NativeMethods.LoadNvApi64();
                    else
                        NativeMethods.LoadNvApi32();
                }
                catch { } // will always fail since 'fake' entry point doesn't exists
            }
        };

        internal static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("nvapi64.dll", EntryPoint = "fake")]
            internal static extern int LoadNvApi64();

            [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
            internal static extern int LoadNvApi32();
        }
    }
}
