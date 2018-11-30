using Infrastructure.CrossCutting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Infrastructure.CrossCutting.Core.Validadores;

namespace Domain.MainModule.Entities {
    public class Cliente : EntityBase {
        [Required]
        [DataMember]
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

        [Required]
        [MaxLength(80)]
        [Display(Name = "Razon Social")]
        [DataMember]
        public string RazonSocial {
            get { return razonSocial; }
            set {
                if (razonSocial != value) {
                    razonSocial = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string razonSocial;

        [MaxLength(15)]
        [DataMember]
        public string CIF {
            get { return cif; }
            set {
                if (cif != value) {
                    cif = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string cif;

        [MaxLength(250)]
        [DataMember]
        public string Direccion {
            get { return direccion; }
            set {
                if (direccion != value) {
                    direccion = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string direccion;

        [MaxLength(50)]
        [DataMember]
        public string Localidad {
            get { return localidad; }
            set {
                if (localidad != value) {
                    localidad = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string localidad;

        [MaxLength(50)]
        [DataMember]
        public string Provincia {
            get { return provincia; }
            set {
                if (provincia != value) {
                    provincia = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string provincia;

        [MaxLength(10)]
        [RegularExpression(@"^\d{1,5}$")]
        [Display(Name = "Código Postal")]
        [DataMember]
        public string CP {
            get { return cp; }
            set {
                if (cp != value) {
                    cp = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string cp;

        [MaxLength(250)]
        [DataMember]
        public string Pais {
            get { return pais; }
            set {
                if (pais != value) {
                    pais = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string pais;

        [MaxLength(250)]
        [EmailAddress]
        [DataMember]
        public string eMail {
            get { return _eMail; }
            set {
                if (_eMail != value) {
                    _eMail = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _eMail;

        [DataMember]
        public bool Moroso {
            get { return moroso; }
            set {
                if (moroso != value) {
                    moroso = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool moroso=false;

        public override string this[string columnName] {
            get {
                switch (columnName) {
                    case nameof(CIF):
                        if (CIF.EstaRelleno() && CIF.NotIsCIF()) {
                            return "No es un CIF valido.";
                        }
                        break;
                }
                return base[columnName];
            }
        }
    }
}
