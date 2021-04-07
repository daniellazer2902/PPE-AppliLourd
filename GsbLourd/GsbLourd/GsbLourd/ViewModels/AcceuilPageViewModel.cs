using Prism.Navigation;
using Prism.Commands;
using System;

namespace GsbLourd.ViewModels
{
    public class AcceuilPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public AcceuilPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

        public String Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private String _id;

        public String Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }
        private String _nom;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = (string)parameters["id"];
            Nom = (string)parameters["nom"];

            base.OnNavigatedTo(parameters);

        }

        //NAVIGATION

        //MEDICAMENTS
        public DelegateCommand MedicamentsCommand
        {
            get { return _medicamentsCommand ?? (_medicamentsCommand = new DelegateCommand(ExecuteMedicamentsCommand, CanExecuteMedicamentsCommand)); }
        }
        private DelegateCommand _medicamentsCommand;

        public async void ExecuteMedicamentsCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Id);
            navigationParameters.Add("nom", Nom);

            await _navigationService.NavigateAsync("MedicamentsPage", navigationParameters);
        }
        public virtual bool CanExecuteMedicamentsCommand()
        {
            return true;
        }

        //VISITEURS
        public DelegateCommand VisiteursCommand
        {
            get { return _visiteursCommand ?? (_visiteursCommand = new DelegateCommand(ExecuteVisiteursCommand, CanExecuteVisiteursCommand)); }
        }
        private DelegateCommand _visiteursCommand;

        public async void ExecuteVisiteursCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Id);
            navigationParameters.Add("nom", Nom);

            await _navigationService.NavigateAsync("RechercheVisiteursPage", navigationParameters);
        }
        public virtual bool CanExecuteVisiteursCommand()
        {
            return true;
        }

        //RAPPORT DE VISITE
        public DelegateCommand RapportVisiteCommand
        {
            get { return _rapportVisiteCommand ?? (_rapportVisiteCommand = new DelegateCommand(ExecuteRapportVisiteCommand, CanExecuteRapportVisiteCommand)); }
        }
        private DelegateCommand _rapportVisiteCommand;

        public async void ExecuteRapportVisiteCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Id);
            navigationParameters.Add("nom", Nom);

            await _navigationService.NavigateAsync("RapportVisitePage", navigationParameters);
        }
        public virtual bool CanExecuteRapportVisiteCommand()
        {
            return true;
        }

        //PRATICIENS
        public DelegateCommand PraticiensCommand
        {
            get { return _praticiensCommand ?? (_praticiensCommand = new DelegateCommand(ExecutePraticiensCommand, CanExecutePraticiensCommand)); }
        }
        private DelegateCommand _praticiensCommand;

        public async void ExecutePraticiensCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", Id);
            navigationParameters.Add("nom", Nom);

            await _navigationService.NavigateAsync("RecherchePraticiensPage", navigationParameters);
        }
        public virtual bool CanExecutePraticiensCommand()
        {
            return true;
        }
    }
}