﻿// <auto-generated />
using System;
using FourthPro.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FourthPro.Database.Migrations
{
    [DbContext(typeof(FourthProDbContext))]
    partial class FourthProDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FourthPro.Database.Model.DepartmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("FourthPro.Database.Model.DoctorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("FourthPro.Database.Model.LectureModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("File")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsPractical")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lecture");
                });

            modelBuilder.Entity("FourthPro.Database.Model.ProjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Tools")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("FourthPro.Database.Model.StudentSubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentSubject");
                });

            modelBuilder.Entity("FourthPro.Database.Model.SubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("File")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Semester")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("FourthPro.Database.Model.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<int?>("FifthProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("FourthProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Identifier")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FifthProjectId");

                    b.HasIndex("FourthProjectId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("FourthPro.Database.Model.DoctorModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.DepartmentModel", "Department")
                        .WithMany("Doctors")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("FourthPro.Database.Model.LectureModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.SubjectModel", "Subject")
                        .WithMany("Lecture")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("FourthPro.Database.Model.ProjectModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.DoctorModel", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("FourthPro.Database.Model.StudentSubjectModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.SubjectModel", "Subject")
                        .WithMany("StudentSubject")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FourthPro.Database.Model.UserModel", "User")
                        .WithMany("StudentSubject")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FourthPro.Database.Model.SubjectModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.DoctorModel", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("FourthPro.Database.Model.UserModel", b =>
                {
                    b.HasOne("FourthPro.Database.Model.ProjectModel", "FifthProject")
                        .WithMany()
                        .HasForeignKey("FifthProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FourthPro.Database.Model.ProjectModel", "FourthProject")
                        .WithMany("Users")
                        .HasForeignKey("FourthProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("FifthProject");

                    b.Navigation("FourthProject");
                });

            modelBuilder.Entity("FourthPro.Database.Model.DepartmentModel", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("FourthPro.Database.Model.ProjectModel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("FourthPro.Database.Model.SubjectModel", b =>
                {
                    b.Navigation("Lecture");

                    b.Navigation("StudentSubject");
                });

            modelBuilder.Entity("FourthPro.Database.Model.UserModel", b =>
                {
                    b.Navigation("StudentSubject");
                });
#pragma warning restore 612, 618
        }
    }
}
