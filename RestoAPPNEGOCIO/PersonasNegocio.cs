using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestoAPPBD;

namespace RestoAPPNegocio
{
    public class PersonasNegocio

    {
        public string Rut_Persona { get; set; }

        public string filtrar { get; set; }

        public string Rut_Persona_val { get; set; }
        public string Nom_Persona { get; set; }
        public string Apel_Pat_Persona { get; set; }
        public string Apel_Mat_Persona { get; set; }
        public string Fecha_Nac { get; set; }
        public string Pass { get; set; }
        public string Id_Rol { get; set; }
        public string Id_Cargo { get; set; }
        public string Email { get; set; }


        public bool Login()
        {

                return new PersonasBD().Login(this.Rut_Persona, this.Pass);
  
                
        }

        public int Agregar()
        {
            return new PersonasBD().Agregar(this.Rut_Persona, this.Nom_Persona, this.Apel_Pat_Persona, this.Apel_Mat_Persona, this.Pass,
                                            this.Fecha_Nac, this.Id_Rol, this.Id_Cargo, this.Email);
        }

        public DataTable ListarPersonas()
        {
            return  new PersonasBD().ListarPersonas();
        }

        public int ModificarPersonas()
        {
            return new PersonasBD().ModificarPersonas(this.Rut_Persona, this.Nom_Persona, this.Apel_Pat_Persona, this.Apel_Mat_Persona, this.Pass,
                                            this.Fecha_Nac, this.Id_Rol, this.Id_Cargo , this.Email);
        }

        public int Eliminar()
        {
            return new PersonasBD().Eliminar(this.Rut_Persona);
        }

        public DataTable ListarPorRUT()
        {
            return new PersonasBD().ListarPorRUT(this.Rut_Persona);
        }

        public DataTable Validacion()
        {
            return new PersonasBD().Validacion(this.Rut_Persona_val);
        }

        public DataTable Filtrar()
        {
            return new PersonasBD().Filtrado(this.filtrar);
        }
    }
}
