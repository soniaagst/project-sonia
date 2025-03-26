﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingSystemLibrary.Data;

#nullable disable

namespace ParkingSystemLibrary.Migrations
{
    [DbContext(typeof(VehicleDb))]
    partial class VehicleDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("ParkingSystemLibrary.Models.Vehicle", b =>
                {
                    b.Property<string>("LicensePlate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Owner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("LicensePlate");

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
