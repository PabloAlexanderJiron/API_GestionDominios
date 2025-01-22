using Aplicacion.Dominio.Entidades.DominioInternet;
using Aplicacion.Dominio.Entidades.DominioInternet.Errores;
using Bogus;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Unitario.Test.Dominio.Entidades
{
    public class DominioInternetTests
    {
        private readonly Faker faker;
        public DominioInternetTests()
        {
            faker = new Faker();
        }
        [Fact]
        public void Crear_RetornaDominioIntener()
        {

            var direccion = faker.Internet.DomainName();
            var proveedor = faker.Company.CompanyName();
            var fechaCompra = faker.Date.RecentOffset();
            var fechaRenovacion = faker.Date.FutureOffset(1);
            var precio = faker.Random.Double(2,10);
            var idUsuario = 3;
            var sut = DominioInternet.Crear(direccion, proveedor, fechaCompra, precio, fechaRenovacion, idUsuario);
            sut.Id.ShouldBe(0);
            sut.Direccion.ShouldBe(direccion);
            sut.Proveedor.ShouldBe(proveedor);
            sut.FechaCompra.ShouldBe(fechaCompra);
            sut.Precio.ShouldBe(precio);
            sut.FechaRenovacion.ShouldBe(fechaRenovacion);
            sut.IdUsuario.ShouldBe(idUsuario);
        }

        [Fact]
        public void Crear_CuandoLaFechaRenoivacionEsMayorQueLaDeCompra_RetornaError()
        {

            var direccion = faker.Internet.DomainName();
            var proveedor = faker.Company.CompanyName();
            var fechaRenovacion = faker.Date.RecentOffset();
            var fechaCompra = fechaRenovacion.AddYears(1);
            var precio = faker.Random.Double(2, 10);
            var idUsuario = 3;
            Should.Throw<ErroresDominioInternet.FechaRenovacionMayorQueLaCompra>(() => DominioInternet.Crear(direccion, proveedor, fechaCompra, precio, fechaRenovacion, idUsuario));
        }

        [Fact]
        public void Actualizar_CuandoLaFechaRenoivacionEsMayorQueLaDeCompra_RetornaError()
        {

            var direccion = faker.Internet.DomainName();
            var proveedor = faker.Company.CompanyName();
            var fechaCompra = faker.Date.RecentOffset();
            var fechaRenovacion = faker.Date.FutureOffset(1);
            var precio = faker.Random.Double(2, 15);
            var idUsuario = 3;
            var sut = DominioInternet.Crear(direccion, proveedor, fechaCompra, precio, fechaRenovacion, idUsuario);

            var proveedorEsperado = faker.Company.CompanyName();
            var fechaCompraEsperada = faker.Date.RecentOffset();
            var fechaRenovacionEsperada = faker.Date.PastOffset(1);
            var precioEsperado = faker.Random.Double(2, 15);

            Should.Throw<ErroresDominioInternet.FechaRenovacionMayorQueLaCompra>(() => sut.Actualizar(proveedorEsperado, fechaCompraEsperada, precioEsperado, fechaRenovacionEsperada));
        }

        [Fact]
        public void Actualizar_ActualizaLosCampos()
        {

            var direccion = faker.Internet.DomainName();
            var proveedor = faker.Company.CompanyName();
            var fechaCompra = faker.Date.RecentOffset();
            var fechaRenovacion = faker.Date.FutureOffset(1);
            var precio = faker.Random.Double(2, 15);
            var idUsuario = 3;
            var sut = DominioInternet.Crear(direccion, proveedor, fechaCompra, precio, fechaRenovacion, idUsuario);

            var proveedorEsperado = faker.Company.CompanyName();
            var fechaCompraEsperada = faker.Date.RecentOffset();
            var fechaRenovacionEsperada = faker.Date.FutureOffset(1);
            var precioEsperado = faker.Random.Double(2, 15);

            sut.Actualizar(proveedorEsperado, fechaCompraEsperada, precioEsperado, fechaRenovacionEsperada);
            sut.Direccion.ShouldBe(direccion);
            sut.Proveedor.ShouldBe(proveedorEsperado);
            sut.FechaCompra.ShouldBe(fechaCompraEsperada);
            sut.Precio.ShouldBe(precioEsperado);
            sut.FechaRenovacion.ShouldBe(fechaRenovacionEsperada);
            sut.IdUsuario.ShouldBe(idUsuario);
        }

        [Fact]
        public void Eliminar_EliminaCorrectamente()
        {
            var direccion = faker.Internet.DomainName();
            var proveedor = faker.Company.CompanyName();
            var fechaCompra = faker.Date.RecentOffset();
            var fechaRenovacion = faker.Date.FutureOffset(1);
            var precio = faker.Random.Double(2, 15);
            var idUsuario = 3;
            var sut = DominioInternet.Crear(direccion, proveedor, fechaCompra, precio, fechaRenovacion, idUsuario);

            sut.Eliminar();
            sut.Eliminado.ShouldBe(true);
        }
    }
}
