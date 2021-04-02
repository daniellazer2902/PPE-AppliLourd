﻿using Newtonsoft.Json.Linq;
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

        public string Recherche
        {
            get { return _recherche; }
            set { SetProperty(ref _recherche, value); }
        }
        private string _recherche;

        public List<string> Visiteurs
        {
            get { return _visiteurs; }
            set { SetProperty(ref _visiteurs, value); }
        }
        private List<string> _visiteurs;

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

                foreach (JObject visiteur in Visiteurs)
                {
                    Visiteurs.Add((string)visiteur["VIS_NOM"] + (string)visiteur["VIS_PRENOM"]);
                }

            }
        }
    }
}