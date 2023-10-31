using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ServiceInterfaces
{
    public interface ICategoryService
    {
        public List<Category> ListCategories();
    }
}
