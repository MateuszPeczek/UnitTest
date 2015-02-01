using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.Repository
{
    public interface IBlogRepository : IGenericRepository<BlogPost>
    {
        BlogPost SelectById(int? id);
    }
}
