using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Repository
{
    public interface IBlogRepository
    {
        IEnumerable<BlogPost> SelectAll();
        BlogPost SelectByID(int? id);
        void Create(BlogPost obj);
        void Update(BlogPost obj);
        void Delete(int? id);
        void Save();
    }
}
