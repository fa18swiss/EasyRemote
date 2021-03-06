﻿using System.Windows;
using EasyRemote.Impl.Module;
using Microsoft.Practices.Unity;

namespace EasyRemote
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            new ProgramsProtocols.Module().Load(container);
            new ImplModule().Load(container);

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}