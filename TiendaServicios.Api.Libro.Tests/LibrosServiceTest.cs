using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.Api.Libro.Modelo;
using Xunit;
using System.Threading;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid().ToString(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty.ToString();

            return lista;

        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var data = this.ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>(); // caracteristicas que debe tener un objeto de entity
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                 .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                 .Returns(new AsyncEnumerator<LibreriaMaterial>(data.GetEnumerator()));
            // COMO ESTE DATO ES MOCK, DEBEMOS SETEARSELO PARA QUE LOS TENGA

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(data.Provider));

            // creo contexto con el dbSet de mi objeto
            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            // Emula un objecto para poder utilizarlo
            var mockContext = this.CrearContexto();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            var mapper = mapConfig.CreateMapper();


            // Instancio mi clase la cual va a tener la logica a ejecutar
            Consulta.Manejador libroManejador = new Consulta.Manejador(mockContext.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await libroManejador.Handle(request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GetLibroById()
        {
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            var mapper = mapConfig.CreateMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GuardarLibro()
        {
            // creo una base de datos en memoria que va a exitir en tiempo de ejecucion
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                             .UseInMemoryDatabase(databaseName: "BaseDatosLibros")
                             .Options;

            var contexto = new ContextoLibreria(options); // instancio el dbContext pasandole el options de la nueva db en memoria

            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de microservice";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.NotNull(libro);
        }

    }
}

