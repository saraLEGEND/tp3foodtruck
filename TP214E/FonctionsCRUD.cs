//using System;
//using System.Collections.Generic;
//using System.Text;
//using MongoDB.Driver;
//using TP214E.Data;

//namespace TP214E
//{
//    public class FonctionsCRUD
//    {
//        public IMongoCollection<Aliment> collectionAliment;
//        public IMongoCollection<Commande> collectionCommande;
//        public IMongoCollection<Recette> collectionRecettes;

//        AccesDonnees dal = new AccesDonnees();

//        public List<Aliment> ObtenirCollectionAliments()
//        {
//            List<Aliment> listeAliments = new List<Aliment>();
//            try
//            {
//                collectionAliment = dal.baseDonnees.GetCollection<Aliment>("Aliments");
//                listeAliments = collectionAliment.Aggregate().ToList();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }

//            return listeAliments;
//        }

//        public List<Commande> ObtenirCollectionCommandes()
//        {
//            List<Commande> listeCommandes;
//            try
//            {
//                collectionCommande = dal.baseDonnees.GetCollection<Commande>("Commandes");
//                listeCommandes = collectionCommande.Aggregate().ToList();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//                throw;
//            }

//            return listeCommandes;
//        }

//        public List<Recette> ObtenirCollectionRecettes()
//        {
//            List<Recette> listeRecettes = new List<Recette>();
//            try
//            {
//                collectionRecettes = dal.baseDonnees.GetCollection<Recette>("Recettes");
//                listeRecettes = collectionRecettes.Aggregate().ToList();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }

//            return listeRecettes;
//        }



//        public void CreerCommande(Commande commande)
//        {
//            dal.collectionCommande.InsertOne(commande);
//        }

//    }
//}
