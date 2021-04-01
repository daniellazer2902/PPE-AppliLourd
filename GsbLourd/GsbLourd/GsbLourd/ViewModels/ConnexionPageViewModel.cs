using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GsbLourd.ViewModels
{
    public class ConnexionPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ConnexionPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Connexion Page";
        }

        public String Identifiant
        {
            get { return _identifiant; }
            set { SetProperty(ref _identifiant, value); }
        }
        private String _identifiant;

        public String MotDePasse
        {
            get { return _motDePasse; }
            set { SetProperty(ref _motDePasse, value); }
        }
        private String _motDePasse;

        public DelegateCommand ConnexionCommand
        {
            get { return _connexionCommand ?? (_connexionCommand = new DelegateCommand(ExecuteConnexionCommand, CanExecuteConnexionCommand)); }
        }
        private DelegateCommand _connexionCommand;

        public async void ExecuteConnexionCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();


            navigationParameters.Add("motdepasse", MotDePasse);
            navigationParameters.Add("identifiant", Identifiant);

            await _navigationService.NavigateAsync("RapportVisitePage", navigationParameters);
        }

        public virtual bool CanExecuteConnexionCommand()
        {
            return true;
        }
    }
}
