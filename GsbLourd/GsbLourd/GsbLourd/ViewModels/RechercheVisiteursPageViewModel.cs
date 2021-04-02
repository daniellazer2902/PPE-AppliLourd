using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Visiteur SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        private Visiteur _selectedItem;

        public ObservableCollection<Visiteur> Visiteurs
        {
            get { return _visiteurs; }
            set { SetProperty(ref _visiteurs, value); }
        }
        private ObservableCollection<Visiteur> _visiteurs;

        public DelegateCommand SearchVisiteurCommand
        {
            get { return _searchVisiteurCommand ?? (_searchVisiteurCommand = new DelegateCommand(ExecuteSearchVisiteurCommand, CanExecuteSearchVisiteurCommand)); }
        }
        private DelegateCommand _searchVisiteurCommand;

        public async void ExecuteSearchVisiteurCommand()
        {
            try
            {
                Visiteurs = new ObservableCollection<Visiteur>();

                Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetVisisteur.php?RECHERCHE=" + Recherche);
                Console.WriteLine("{0} URI", uri);

                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Test 1");
                    var answer = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("{0} Test 2", answer);

                    Console.WriteLine("Test 3");
                    dynamic dynJson = JsonConvert.DeserializeObject(answer);

                    Console.WriteLine("{0} Test 3", dynJson);

                    foreach (var visiteur in dynJson)
                    {

                        Console.WriteLine("{0} JSON", visiteur.VIS_NOM);
                        Visiteur _visiteur = new Visiteur();

                        Console.WriteLine("{0} NOM", visiteur.VIS_NOM);
                        _visiteur.Nom = visiteur.VIS_NOM;
                        _visiteur.Prenom = visiteur.VIS_PRENOM;
                        _visiteur.Id = visiteur.VIS_MATRICULE;

                        Visiteurs.Add(_visiteur);
                    }

                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur, la recherche est vide");
            }
        }
        public virtual bool CanExecuteSearchVisiteurCommand()
        {
            return true;
        }

        
        //NAVIGATE TO THE RESULT
        public DelegateCommand NavigateRechercheResultCommand
        {
            get { return _navigateRechercheResultCommand ?? (_navigateRechercheResultCommand = new DelegateCommand(ExecuteNavigateRechercheResultCommand, CanExecuteNavigateRechercheResultCommand)); }
        }
        private DelegateCommand _navigateRechercheResultCommand;

        public async void ExecuteNavigateRechercheResultCommand()
        {
            NavigationParameters navigationParameters = new NavigationParameters();

            navigationParameters.Add("id", SelectedItem.Id);

            await _navigationService.NavigateAsync("VisiteursPage", navigationParameters);
        }
        public virtual bool CanExecuteNavigateRechercheResultCommand()
        {
            return true;
        }

        public class Visiteur
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Id { get; set; }
        }
    }
}