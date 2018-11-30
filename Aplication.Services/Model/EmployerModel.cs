using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Core;
using Domain.MainModule.Entities;

namespace Aplication.Services.Model {
    public class EmployeerModel: ObservableBase {
        private Employee entity;

        public Employee Entity {
            get {
                return entity;
            }

            set {
                entity = value;
                NotifyPropertyChanged();
            }
        }

        public string Name {
            get {
                return entity.Name;
            }
        }
    }
}
