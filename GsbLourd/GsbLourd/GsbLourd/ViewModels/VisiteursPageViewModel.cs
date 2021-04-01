using Prism.Navigation;
using System;

namespace GsbLourd.ViewModels
{
    public class VisiteursPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public VisiteursPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

    }
}