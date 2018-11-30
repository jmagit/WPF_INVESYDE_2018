using Domain.MainModule.Entities;
using Infrastructure.Data.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Contracts {
    public interface ICategoryRepository : IRepository<Category, int> {
    }
    public interface IReportRepository : IRepository<Report, int> {
    }
}
