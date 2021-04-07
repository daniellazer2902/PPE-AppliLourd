using GsbLourd.ViewModels;
using GsbLourd.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GsbLourd
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/ConnexionPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ConnexionPage, ConnexionPageViewModel>();
            containerRegistry.RegisterForNavigation<RapportVisitePage, RapportVisitePageViewModel>();
            containerRegistry.RegisterForNavigation<CreationRapportVisitePage, CreationRapportVisitePageViewModel>();
            containerRegistry.RegisterForNavigation<AcceuilPage, AcceuilPageViewModel>();
            containerRegistry.RegisterForNavigation<VisiteursPage, VisiteursPageViewModel>();
            containerRegistry.RegisterForNavigation<RechercheVisiteursPage, RechercheVisiteursPageViewModel>();
            containerRegistry.RegisterForNavigation<RecherchePraticiensPage, RecherchePraticiensPageViewModel>();
            containerRegistry.RegisterForNavigation<MedicamentsPage, MedicamentsPageViewModel>();
            containerRegistry.RegisterForNavigation<PraticiensPage, PraticiensPageViewModel>();
        }
    }
}
