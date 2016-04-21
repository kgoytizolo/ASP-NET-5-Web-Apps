using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using WebAppLibrary.Models;

namespace WebAppLibrary.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20160420161404_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAppLibrary.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<int?>("ShoppingCartId");

                    b.Property<double>("UnitPrice");

                    b.Property<string>("UserName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAppLibrary.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("UserName");

                    b.Property<double>("total");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("WebAppLibrary.Models.Book", b =>
                {
                    b.HasOne("WebAppLibrary.Models.ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId");
                });
        }
    }
}
