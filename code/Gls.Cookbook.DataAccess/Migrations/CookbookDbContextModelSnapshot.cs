﻿// <auto-generated />
using System;
using Gls.Cookbook.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gls.Cookbook.DataAccess.Migrations
{
    [DbContext(typeof(CookbookDbContext))]
    partial class CookbookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.IngredientEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.MeasurementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("TEXT");

                    b.Property<int>("MeasurementSystem")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeasurementType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeIngredientEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MeasurementId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecipeSectionEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("MeasurementId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("RecipeSectionEntityId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeInstructionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Instruction")
                        .HasColumnType("TEXT");

                    b.Property<int>("LineNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecipeSectionEntityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("RecipeSectionEntityId");

                    b.ToTable("RecipeInstructions");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeNoteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeNotes");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeSectionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Order")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSections");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeIngredientEntity", b =>
                {
                    b.HasOne("Gls.Cookbook.DataAccess.Models.IngredientEntity", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId");

                    b.HasOne("Gls.Cookbook.DataAccess.Models.MeasurementEntity", "Measurement")
                        .WithMany()
                        .HasForeignKey("MeasurementId");

                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeEntity", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeSectionEntity", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeSectionEntityId");

                    b.Navigation("Ingredient");

                    b.Navigation("Measurement");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeInstructionEntity", b =>
                {
                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeEntity", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeSectionEntity", null)
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeSectionEntityId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeNoteEntity", b =>
                {
                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeEntity", "Recipe")
                        .WithMany("Notes")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeSectionEntity", b =>
                {
                    b.HasOne("Gls.Cookbook.DataAccess.Models.RecipeEntity", "Recipe")
                        .WithMany("Sections")
                        .HasForeignKey("RecipeId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeEntity", b =>
                {
                    b.Navigation("Notes");

                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Gls.Cookbook.DataAccess.Models.RecipeSectionEntity", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");
                });
#pragma warning restore 612, 618
        }
    }
}