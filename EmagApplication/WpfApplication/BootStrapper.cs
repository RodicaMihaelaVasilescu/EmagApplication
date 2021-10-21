using System;
using System.Windows;
using Prism.Unity;

namespace WpfApplication
{
  /// <summary>      
  /// BootStrapper is responsible for loading prism and initializing Shell.      
  /// </summary>      
  [Obsolete]
  public class BootStrapper : UnityBootstrapper
  {
    #region Overridden Methods      
    /// <summary>      
    /// Entry point to the application      
    /// </summary>      
    /// <param name="runWithDefaultConfiguration"></param>      
    public override void Run(bool runWithDefaultConfiguration)
    {
      base.Run(runWithDefaultConfiguration);
    }

    /// <summary>      
    /// Initializes shell.xaml      
    /// </summary>      
    /// <returns></returns>      
    protected override DependencyObject CreateShell()
    {
      return Container.TryResolve<Shell>();
    }

    /// <summary>      
    /// loads the Shell.xaml      
    /// </summary>      
    protected override void InitializeShell()
    {
      App.Current.MainWindow = (Window)Shell;
      App.Current.MainWindow.Show();
    }

    /// <summary>      
    /// Add view(module) from other assemblies and begins with modularity      
    /// </summary>      
    protected override void ConfigureModuleCatalog()
    {
      base.ConfigureModuleCatalog();
      Type ModuleLocatorType = typeof(ModuleLocators);
      ModuleCatalog.AddModule(new Prism.Modularity.ModuleInfo
      {
        ModuleName = ModuleLocatorType.Name,
        ModuleType = ModuleLocatorType.AssemblyQualifiedName
      });
    }
    #endregion
  }
}