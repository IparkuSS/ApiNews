﻿using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace News.DAL.RepositoryModels.Contracts
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticlesAsync(Guid sectionid, Guid subsectionId);

        Task<Article> GetArticleAsync(Guid subsectionId, Guid id);

        void CreateArticlesForSubsection(Guid sectionid, Guid subsectionId, Article article);

        void DeleteArticle(Article article);
    }
}
