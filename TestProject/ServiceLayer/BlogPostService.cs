using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestProject.Models;
using TestProject.Repository;
using TestProject.UOW;

namespace TestProject.ServiceLayer
{
    public class BlogPostService : EntityService<BlogPost>, IBlogPostService
    {
        IUnitOfWork _unitOfWork;
        IBlogRepository _blogRepository;

        public BlogPostService(IUnitOfWork unitOfWork, IBlogRepository blogRepository) : base(unitOfWork, blogRepository)
        {
            _unitOfWork = unitOfWork;
            _blogRepository = blogRepository;
        }

        public BlogPost GetById(int? Id)
        {
            return _blogRepository.SelectById(Id);
        }
    }
}