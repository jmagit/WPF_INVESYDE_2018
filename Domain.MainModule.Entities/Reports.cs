using Infrastructure.CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainModule.Entities {
    public class Category : EntityBase {
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int id = 0;
        public string Name {
            get { return name; }
            set {
                if (name != value) {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string name;

        // Propiedad de navegación
        public IList<Project> Projects {
            get { return projects; }
            set {
                if (projects != value) {
                    projects = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private IList<Project> projects;

        public Category() {
            Id = 0;
            Name = "Empty Name";
        }
        public Category(int id, string name) : this() {
            Id = id;
            Name = name;
        }
    }

    public class Project : EntityBase {
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int id = 0;
        public string Name {
            get { return name; }
            set {
                if (name != value) {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string name;
        public int CategoryId {
            get { return categoryId; }
            set {
                if (categoryId != value) {
                    categoryId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int categoryId = 0;

        // Propiedades de navegación
        public Category Category {
            get { return category; }
            set {
                if (category != value) {
                    category = value;
                    if (value != null) CategoryId = category.Id;
                    NotifyPropertyChanged();
                }
            }
        }
        private Category category;
        public IList<Employee> Employees {
            get { return employees; }
            set {
                if (employees != value) {
                    employees = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private IList<Employee> employees;

        public Project() {
            Id = 0;
            Name = "Empty Name";
            CategoryId = 0;
        }
        public Project(int id, string name, int categoryId) : this() {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }
    }

    public class Employee : EntityBase {
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int id = 0;
        public string Name {
            get { return name; }
            set {
                if (name != value) {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string name;
 
        // Propiedad de navegación
        public IList<Project> Projects {
            get { return projects; }
            set {
                if (projects != value) {
                    projects = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private IList<Project> projects;

        public Employee() {
            Id = 0;
            Name = "Empty Name";
            Projects = new List<Project>();
        }
        public Employee(int id, string name, IList<Project> projects) : this() {
            Id = id;
            Name = name;
            Projects = projects;
        }
    }

    public class Report : EntityBase {
        public int Id {
            get { return id; }
            set {
                if (id != value) {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int id = 0;
        public string Name {
            get { return name; }
            set {
                if (name != value) {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string name;
        public int CategoryId {
            get { return categoryId; }
            set {
                if (categoryId != value) {
                    categoryId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int categoryId = 0;
        public int ProjectId {
            get { return projectId; }
            set {
                if (projectId != value) {
                    projectId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int projectId = 0;
        public IList<Employee> Employees {
            get { return employees; }
            set {
                if (employees != value) {
                    employees = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private IList<Employee> employees;

        // Propiedades de navegación
        public Category Category {
            get { return category; }
            set {
                if (category != value) {
                    category = value;
                    if (value != null) CategoryId = category.Id;
                    NotifyPropertyChanged();
                }
            }
        }
        private Category category;
        public Project Project {
            get { return project; }
            set {
                if (project != value) {
                    project = value;
                    if(value != null) ProjectId = project.Id;
                    NotifyPropertyChanged();
                }
            }
        }
        private Project project;

        public Report() {
            Category = new Category();
            Project = new Project();
            Employees = new ObservableCollection<Employee>();
        }


    }
}
