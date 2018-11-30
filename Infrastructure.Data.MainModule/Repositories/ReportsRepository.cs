using Domain.MainModule.Entities;
using Infrastructure.Data.Contracts;
using Infrastructure.Data.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainModule.Repositories {
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository {
        static CategoryRepository() {
            int idP = 1, idE=1;
            for(int i = 1; i < 5; i++) {
                var c = new Category(i, $"Categoria {i}");
                c.Projects = new List<Project>();
                store.Add(i, c);
                for (int j = 1; j < 3; j++, idP++) {
                    var p = new Project(idP, $"Proyecto {idP}", i);
                    p.Category = c;
                    p.Employees = new List<Employee>();
                    c.Projects.Add(p);
                    for (int k = 1; k < 10; k++, idE++) {
                        var e = new Employee(idE, $"Empleado {idE} {idP}", new List<Project>());
                        (e.Projects as List<Project>).Add(p);
                        p.Employees.Add(e);
                    }
                }
            }
        }
        public CategoryRepository() : base(nameof(Category.Id)) {

        }
    }
    public class ReportRepository : Repository<Report, int>, IReportRepository {
        static ReportRepository() {
        }
        public ReportRepository() : base(nameof(Report.Id)) {

        }
    }
}
