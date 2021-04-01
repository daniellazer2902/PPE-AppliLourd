using Prism.Navigation;
using System;

namespace GsbLourd.ViewModels
{
    public class MedicamentsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public MedicamentsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

    }
}