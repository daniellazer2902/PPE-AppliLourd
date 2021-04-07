using Newtonsoft.Json.Linq;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace GsbLourd.ViewModels
{
    public class PraticiensPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public PraticiensPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _client = new HttpClient();
            _navigationService = navigationService;
            Title = "Praticiens";
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

        public string Coef
        {
            get { return _coef; }
            set { SetProperty(ref _coef, value); }
        }
        private string _coef;

        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        private string _type;

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            Id = (string)parameters["id"];

            base.OnNavigatedTo(parameters);

            Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetPracticien.php?PRA_NUM=" + Id);
            Console.WriteLine("{0} URI", uri);

            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var answer = await response.Content.ReadAsStringAsync();

                var Answer = JObject.Parse(answer);
                Nom = (string)Answer["PRA_NOM"];
                Prenom = (string)Answer["PRA_PRENOM"];
                Adresse = (string)Answer["PRA_ADRESSE"];
                Cp = (string)Answer["PRA_CP"];
                Ville = (string)Answer["PRA_VILLE"];
                Coef = (string)Answer["PRA_COEFNOTORIETE"];
                Type = (string)Answer["TYP_CODE"];
            }

        }

    }
}