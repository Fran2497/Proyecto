using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.DAL;

namespace BLL
{
    public class clsConsulta
    {
        public List<ProvinciasResult> ConsultarProvincia()
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<ProvinciasResult> datos = db.Provincias().ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CantonesResult> ConsultarCanton(char Provincia)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<CantonesResult> datos = db.Cantones(Provincia).ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<DistritosResult> ConsultarDistrito(char Provincia, string Canton)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                List<DistritosResult> datos = db.Distritos(Provincia, Canton).ToList();
                return datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ProvinciaDescResult ConsultaProvincia(char prov)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                ProvinciaDescResult dato = db.ProvinciaDesc(prov).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CantonDescResult ConsultaCanton(char prov, string canton)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                CantonDescResult dato = db.CantonDesc(prov, canton).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DistritoDescResult ConsultaDistrito(char prov, string canton, string distrito)
        {
            try
            {
                DatosDataContext db = new DatosDataContext();
                DistritoDescResult dato = db.DistritoDesc(prov, canton, distrito).FirstOrDefault();
                return dato;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
