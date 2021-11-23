using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP214E.Data;
using TP214E.Enumeration;
using TP214E.Pages;

namespace TP214E
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class PageAccueil : Page
    {
        public static AccesDonnees dal;
        public static List<Recette> listeRecettes;
        public static List<Commande> listeCommandes;
        public static List<Aliment> listeAliments;

        public PageAccueil()
        {
            InitializeComponent();
            dal = new AccesDonnees();
            listeRecettes = dal.ObtenirCollectionRecettes();
            listeCommandes = dal.ObtenirCollectionCommandes();
            listeAliments = dal.ObtenirCollectionAliments();
        }

        private void BoutonInventaire_Click(object sender, RoutedEventArgs e)
        {
            PageInventaire frmInventaire = new PageInventaire(dal);

            this.NavigationService.Navigate(frmInventaire);
        }

        private void BoutonCommandes_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/PageCommandes.xaml", UriKind.Relative), listeRecettes);
        }
    }
}
