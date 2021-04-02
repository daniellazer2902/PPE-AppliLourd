using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace GsbLourd.ViewModels
{
    public class ConnexionPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public ConnexionPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
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

        public Boolean Erreur
        {
            get { return _erreur; }
            set { SetProperty(ref _erreur, value); }
        }
        private Boolean _erreur;

        public DelegateCommand ConnexionCommand
        {
            get { return _connexionCommand ?? (_connexionCommand = new DelegateCommand(ExecuteConnexionCommand, CanExecuteConnexionCommand)); }
        }
        private DelegateCommand _connexionCommand;

        public async void ExecuteConnexionCommand()
        {
            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/Connexion.php?VIS_NOM=" + Identifiant);
            Console.WriteLine("{0} URI", uri);

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var answer = await response.Content.ReadAsStringAsync();

                    var Answer = JObject.Parse(answer);
                    if ((string)Answer["VIS_NOM"] == Identifiant)
                    {
                        NavigationParameters navigationParameters = new NavigationParameters();


                        navigationParameters.Add("id", (string)Answer["VIS_MATRICULE"]);
                        navigationParameters.Add("nom", Identifiant);

                        await _navigationService.NavigateAsync("AcceuilPage", navigationParameters);
                    }

                }
            }
            catch (Exception e)
            {
                Erreur = true;
                Console.WriteLine("{0} Second exception caught.", e);
            }
        }

        public virtual bool CanExecuteConnexionCommand()
        {
            return true;
        }
    }
}
