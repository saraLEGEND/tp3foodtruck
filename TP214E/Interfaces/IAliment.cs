using MongoDB.Bson;
using TP214E.Enumeration;

namespace TP214E.Data
{
    public interface IAliment
    {
        ObjectId Id { get; set; }
        string Nom { get; set; }
        double Quantite { get; set; }
        UniteMesure UniteMesure { get; set; }
        decimal CoutVente { get; set; }
        string ToString();
    }
}