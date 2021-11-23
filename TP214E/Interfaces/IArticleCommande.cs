using MongoDB.Bson;

namespace TP214E.Data
{
    public interface IArticleCommande
    {
        ObjectId Id { get; set; }
        int QuantiteArticle { get; set; }
        Recette Article { get; set; }
        decimal CoutArticle { get; set; }
        decimal CalculerVendantArticle();
        string ToString();
    }
}