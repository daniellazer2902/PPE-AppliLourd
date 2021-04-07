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
    public class RecherchePraticiensPageViewModel : ViewModelBase
    {
        private readonly HttpClient _client;
        private readonly INavigationService _navigationService;
        public RecherchePraticiensPageViewModel(INavigationService navigationService)
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

        public Praticien SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        private Praticien _selectedItem;

        public ObservableCollection<Praticien> Praticiens
        {
            get { return _praticiens; }
            set { SetProperty(ref _praticiens, value); }
        }
        private ObservableCollection<Praticien> _praticiens;

        public DelegateCommand SearchPraticiensCommand
        {
            get { return _searchPraticiensCommand ?? (_searchPraticiensCommand = new DelegateCommand(ExecuteSearchPraticiensCommand, CanExecuteSearchPraticiensCommand)); }
        }
        private DelegateCommand _searchPraticiensCommand;

        public async void ExecuteSearchPraticiensCommand()
        {
            try
            {
                Praticiens = new ObservableCollection<Praticien>();

                Uri uri = new Uri("https://hugocabaret.onthewifi.com/GSB/APIGSB/requetes/GetPracticien.php?PRA_NOM=" + Recherche);
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

                    foreach (var praticien in dynJson)
                    {

                        Console.WriteLine("{0} JSON", praticien.PRA_NOM);
                        Praticien _praticien = new Praticien();

                        Console.WriteLine("{0} NOM", praticien.PRA_NOM);
                        _praticien.Nom = praticien.PRA_NOM;
                        _praticien.Prenom = praticien.PRA_PRENOM;
                        _praticien.Id = praticien.PRA_NUM;

                        Praticiens.Add(_praticien);
                    }

                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur, la recherche est vide");
            }
        }
        public virtual bool CanExecuteSearchPraticiensCommand()
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

            await _navigationService.NavigateAsync("PraticiensPage", navigationParameters);
        }
        public virtual bool CanExecuteNavigateRechercheResultCommand()
        {
            return true;
        }

        public class Praticien
        {
            public string Nom { get; set; }
            public string Prenom { get; set; }
            public string Id { get; set; }
        }
    }
}