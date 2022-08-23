﻿using CrudMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudMongo.Services
{
    public class ProdutoServices
    {
        private readonly IMongoCollection<Produto> _produtosCollection;

        public ProdutoServices(IOptions<ProdutoDatabaseSettings> produtoServices)
        {
            var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

            _produtosCollection = mongoDatabase.GetCollection<Produto>
                (produtoServices.Value.ProdutoCollectionName);
        }

        public async Task<List<Produto>> GetAsync() =>
            await _produtosCollection.Find(x => true).ToListAsync();

        public async Task<Produto> GetAsync(string id) =>
           await _produtosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Produto produto) =>
            await _produtosCollection.InsertOneAsync(produto);

        public async Task UpdateAsync(string id, Produto produto) =>
           await _produtosCollection.ReplaceOneAsync(x => x.Id == id, produto);

        public async Task RemoveAsync(string id) =>
            await _produtosCollection.DeleteOneAsync(x => x.Id == id);
    }
}
