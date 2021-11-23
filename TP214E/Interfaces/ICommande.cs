using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TP214E.Data
{
    public interface ICommande
    {
        ObjectId Id { get; set; }
        int NoCommande { get; set; }
        List<ArticleCommande> ListeArticleCommande { get; set; }
        decimal CoutTotalCommande { get; set; }
        DateTime DateCommande { get; set; }
        decimal CalculerVendantCommande();
        string ToString();
    }
}