using Newtonsoft.Json.Linq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GsbLourd.ViewModels
{
    public class VisiteursPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public VisiteursPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Rapport de visite Page";
        }
        
        public string Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _id;

        public string Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }
        private string _nom;

        public string Prenom
        {
            get { return _prenom; }
            set { SetProperty(ref _prenom, value); }
        }
        private string _prenom;

        public string Adresse
        {
            get { return _adresse; }
            set { SetProperty(ref _adresse, value); }
        }
        private string _adresse;

        public string Cp
        {
            get { return _cp; }
            set { SetProperty(ref _cp, value); }
        }
        private string _cp;

        public string Ville
        {
            get { return _ville; }
            set { SetProperty(ref _ville, value); }
        }
        private string _ville;
        
        public string DateEmbauche
        {
            get { return _dateEmbauche; }
            set { SetProperty(ref _dateEmbauche, value); }
        }
        private string _dateEmbauche;

        public string Secteur
        {
            get { return _secteur; }
            set { SetProperty(ref _secteur, value); }
        }
        private string _secteur;

        public string Labo
        {
            get { return _labo; }
            set { SetProperty(ref _labo, value); }
        }
        private string _labo;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = (string)parameters["id"];

            base.OnNavigatedTo(parameters);

            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetVisisteur.php?VIS_MATRICULE=" + Id);
            Console.WriteLine("{0} URI", uri);

            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();

                var Answer = JObject.Parse(answer);
                Nom = (string)Answer["VIS_NOM"];
                Prenom = (string)Answer["VIS_PRENOM"];
                Adresse = (string)Answer["VIS_ADRESSE"];
                Cp = (string)Answer["VIS_CP"];
                Ville = (string)Answer["VIS_VILLE"];
                DateEmbauche = (string)Answer["VIS_DATEEMBAUCHE"];
                Secteur = (string)Answer["SEC_CODE"];
                Labo = (string)Answer["LAB_CODE"];
            }

        }

    }
}