using Newtonsoft.Json.Linq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GsbLourd.ViewModels
{
    public class RechercheVisiteursPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public RechercheVisiteursPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }

        public string Recherche
        {
            get { return _recherche; }
            set { SetProperty(ref _recherche, value); }
        }
        private string _recherche;

        public List<Visiteur> Visiteurs
        {
            get { return _visiteurs; }
            set { SetProperty(ref _visiteurs, value); }
        }
        private List<Visiteur> _visiteurs;

        public async void SearchVisiteurCommand()
        {
            Uri uri = new Uri("https://https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetVisisteur.php?RECHERCHE=" + Recherche);
            Console.WriteLine("{0} URI", uri);

            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();

                var Answer = JObject.Parse(answer);

                Visiteurs.Clear();

                foreach (JObject visiteur in Answer[0])
                {
                    Visiteur _visiteur = new Visiteur();

                    _visiteur.Nom = (string)visiteur["VIS_NOM"];
                    _visiteur.Prenom = (string)visiteur["VIS_PRENOM"];
                    _visiteur.Id = (string)visiteur["VIS_MATRICULE"];

                    Visiteurs.Add(_visiteur);
                }

            }
        }

        public class Visiteur
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Id { get; set; }
        }
    }
}