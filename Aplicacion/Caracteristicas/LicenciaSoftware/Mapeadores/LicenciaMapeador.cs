using Aplicacion.Dominio.Entidades.LicenciaSoftware;
using Aplicacion.DTOs;
using AutoMapper;
using LicenciaSoftwareDominio = Aplicacion.Dominio.Entidades.LicenciaSoftware.LicenciaSoftware;

namespace Aplicacion.Caracteristicas.LicenciaSoftware.Mapeadores
{
    public class LicenciaMapeador: Profile
    {
        public LicenciaMapeador()
        {
            CreateMap<LicenciaSoftwareDominio, LicenciaSoftwareDTO>();
        }
    }
}
