using System;
using System.Collections.Generic;
using System.Windows;
using MongoDB.Driver;

namespace TP214E.Data
{
    public class AccesDonnees : IAccesDonnees
    {
        private MongoClient mongoDBClient;
        private IMongoDatabase baseDonnees;
        private IMongoCollection<Aliment> collectionAliment;
        private IMongoCollection<Commande> collectionCommande;
        private IMongoCollection<Recette> collectionRecettes;

        public AccesDonnees()
        {
            mongoDBClient = OuvrirConnexion();
            baseDonnees = ConnecterBaseDonnees();
        }

        public MongoClient OuvrirConnexion()
        {
            MongoClient dbClient = null;
            try
            {
                dbClient = new MongoClient("mongodb://localhost:27017/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de se connecter à la base de données " + ex.Message, "Erreur",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return dbClient;
        }

        public IMongoDatabase ConnecterBaseDonnees()
        {
            IMongoDatabase bdCourante = null;

            try
            {
                bdCourante = mongoDBClient.GetDatabase("TB2DB");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
            }
            return bdCourante;
        }

        public List<Aliment> ObtenirCollectionAliments()
        {
            List<Aliment> listeAliments = new List<Aliment>();
            try
            {
                collectionAliment = baseDonnees.GetCollection<Aliment>("Aliments");
                listeAliments = collectionAliment.Aggregate().ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return listeAliments;
        }

        public List<Commande> ObtenirCollectionCommandes()
        {
            List<Commande> listeCommandes = new List<Commande>();
            try
            {
                collectionCommande = baseDonnees.GetCollection<Commande>("Commandes");
                listeCommandes = collectionCommande.Aggregate().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return listeCommandes;
        }

        public List<Recette> ObtenirCollectionRecettes()
        {
            List<Recette> listeRecettes = new List<Recette>();
            try
            {
                collectionRecettes = baseDonnees.GetCollection<Recette>("Recettes");
                listeRecettes = collectionRecettes.Aggregate().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return listeRecettes;
        }

        public bool CreerCommande(Commande pCommande)
        {
            try
            {
                collectionCommande.InsertOne(pCommande);
                return true;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Impossible de créer une commande dans la base de donnée");
            }
        }

        public bool CreerAliment(Aliment pAliment)
        {
            try
            {
                collectionAliment.InsertOne(pAliment);
                return true;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Impossible de créer un aliment dans la base de donnée");
            }
        }

        public void SupprimerAliment(Aliment pAliment)
        {
            try
            {
                var alimentRecherche = Builders<Aliment>.Filter.Eq(aliment => aliment.Id, pAliment.Id);
                collectionAliment.DeleteOne(alimentRecherche);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Impossible de supprimer un aliment dans la base de donnée");
            }
        }

        public void MettreAJourAliment(Aliment pAliment)
        {
            var alimentRecherche = Builders<Aliment>.Filter.Eq(aliment => aliment.Id, pAliment.Id);
            var miseAJour = Builders<Aliment>.Update
                .Set("Nom", pAliment.Nom)
                .Set("Quantite", pAliment.Quantite)
                .Set("CoutVente", pAliment.CoutVente)
                .Set("UniteMesure", pAliment.UniteMesure);
            var documentAJour = collectionAliment.UpdateOne(alimentRecherche, miseAJour);
        }
    }
}
