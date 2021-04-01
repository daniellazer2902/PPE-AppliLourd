using Prism.Navigation;
using System;

namespace GsbLourd.ViewModels
{
    public class PraticiensPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public PraticiensPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

    }
}