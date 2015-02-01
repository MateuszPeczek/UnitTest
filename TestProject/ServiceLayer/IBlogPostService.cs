using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;

namespace TestProject.ServiceLayer
{
    public interface IBlogPostService : IEntityService<BlogPost>
    {
        BlogPost GetById(int Id);
    }
}
