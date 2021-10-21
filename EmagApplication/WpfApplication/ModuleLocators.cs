using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace WpfApplication
{
  /// <summary>      
  /// Responsible for mapping modules      
  /// </summary>      
  public class ModuleLocators : IModule
  {
    #region properties      
    /// <summary>      
    /// Instance of IRegionManager      
    /// </summary>      
    private IRegionManager _regionManager;

    #endregion

    #region Constructor      
    /// <summary>      
    /// parameterized constructor initializes IRegionManager      
    /// </summary>      
    /// <param name="regionManager"></param>      
    public ModuleLocators(IRegionManager regionManager)
    {
      _regionManager = regionManager;
    }
    #endregion

    #region Interface methods      
    /// <summary>      
    /// Initializes Welcome page of your application.      
    /// </summary>      
    /// <param name="containerProvider"></param>      
    public void OnInitialized(IContainerProvider containerProvider)
    {
      _regionManager.RegisterViewWithRegion("Shell", typeof(View.MainWindowControl));
    }

    /// <summary>      
    /// RegisterTypes used to register modules      
    /// </summary>      
    /// <param name="containerRegistry"></param>      
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }
    #endregion
  }
}