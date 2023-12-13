using API_BOCHA_STORE.Models;
using Microsoft.EntityFrameworkCore;

namespace API_BOCHA_STORE.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext
            (

                DbContextOptions<ApplicationDBContext> options

            ) : base(options) // Es como un super en herencia
        {



        }

        // Creo la tabla de esta manera en la DB
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proovedor> Proovedor { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // Agregar datos a través de código con esta función
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().HasData(

                new Producto
                {
                    idProducto = 1,
                    nombreProducto = "Camiseta Negra",
                    idProovedor = 1, 
                    idMarca =1,
                    descripcionProducto = "Camiseta Oversized",
                    precio = 19.99,
                    fechaCreacion = DateTime.Parse("2023-12-11")
                },
                new Producto
                {
                    idProducto = 2,
                    nombreProducto = "Pantalon Negro",
                    idProovedor = 2,
                    idMarca =2,
                    descripcionProducto = "Pantalon Deportivo",
                    precio = 15.99,
                    fechaCreacion = DateTime.Parse("2023-12-11")
                }

            );

             
            modelBuilder.Entity<Marca>().HasData(

                new Marca
                {
                    idMarca = 1,
                    nombreMarca = "Adidas",

                },
                new Marca
                {
                    idMarca = 2,
                    nombreMarca = "Nike",

                }

            );

            modelBuilder.Entity<Proovedor>().HasData(

                new Proovedor
                {
                    idProovedor = 1,
                    nombreProovedor = "Fast & Good",
                    duracionContrato = 3,
                    precioImportacion = 90.0
                },
                new Proovedor
                {
                    idProovedor = 2,
                    nombreProovedor = "Import S.A",
                    duracionContrato = 3,
                    precioImportacion = 70.0
                }, new Proovedor
                {
                    idProovedor = 3,
                    nombreProovedor = "Import Facil",
                    duracionContrato = 3,
                    precioImportacion = 65.0
                }
            );


            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    idUsuario = 1,
                    username = "admin",
                    password = "admin123"
                },
                new Usuario
                {
                    idUsuario = 2,
                    username = "cliente",
                    password = "cliente123"
                }
            );

        }

    }
}
