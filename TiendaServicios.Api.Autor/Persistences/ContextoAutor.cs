using System;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Models;

namespace TiendaServicios.Api.Autor.Persistences
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options): base(options)
        {
        }

        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradoAcademicos { get; set; }

    }
}

