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

        public User Utilisateur
        {
            get { return _utilisateur; }
            set { SetProperty(ref _utilisateur, value); }
        }
        private User _utilisateur;
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
        public JObject Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
        }
        private JObject _answer;

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

                    Answer = JObject.Parse(answer);
                    if ((string)Answer["VIS_NOM"] == Identifiant)
                    {
                        NavigationParameters navigationParameters = new NavigationParameters();


                        navigationParameters.Add("motdepasse", MotDePasse);
                        navigationParameters.Add("identifiant", Identifiant);

                        await _navigationService.NavigateAsync("RapportVisitePage", navigationParameters);
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

        public class User
        {
            public string VIS_MATRICULE { get; set; }
            public string VIS_NOM { get; set; }
            public string Vis_PRENOM { get; set; }
            public string VIS_ADRESSE { get; set; }
            public string VIS_VILLE { get; set; }
            public string SEC_CODE { get; set; }
            public string LAB_CODE { get; set; }
            public int VIS_CP { get; set; }
            public string VIS_DATEEMBAUCHE { get; set; }
        }
    }
}
