using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GsbLourd.ViewModels
{
    public class CreationRapportVisitePageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public CreationRapportVisitePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
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

        public String Answer
        {
            get { return _answer; }
            set { SetProperty(ref _answer, value); }
        }
        private String _answer;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/Connexion.php?VIS_NOM=Villechalane");
            
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Answer = await response.Content.ReadAsStringAsync();

            }

            MotDePasse = (string)parameters["motdepasse"];
            Identifiant = (string)parameters["identifiant"];

            base.OnNavigatedTo(parameters);

        }
    }
}