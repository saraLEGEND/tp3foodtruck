using System.Collections.Generic;
using MongoDB.Driver;

namespace TP214E.Data
{
    public interface IAccesDonnees
    {

        MongoClient OuvrirConnexion();
        IMongoDatabase ConnecterBaseDonnees();
        List<Aliment> ObtenirCollectionAliments();
        List<Commande> ObtenirCollectionCommandes();
        List<Recette> ObtenirCollectionRecettes();
        bool CreerCommande(Commande pCommande);
        bool CreerAliment(Aliment pAliment);
        void SupprimerAliment(Aliment pAliment);
        void MettreAJourAliment(Aliment pAliment);

    }
}