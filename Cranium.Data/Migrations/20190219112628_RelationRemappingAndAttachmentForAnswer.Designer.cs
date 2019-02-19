﻿// <auto-generated />
using System;
using Cranium.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cranium.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190219112628_RelationRemappingAndAttachmentForAnswer")]
    partial class RelationRemappingAndAttachmentForAnswer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("Cranium.Data.Models.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Attachment");

                    b.Property<string>("Info");

                    b.Property<bool>("IsCorrect");

                    b.Property<Guid>("QuestionId");

                    b.Property<Guid?>("QuestionId1");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionId1");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Cranium.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Cranium.Data.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Attachment");

                    b.Property<Guid>("QuestionTypeId");

                    b.Property<Guid?>("QuestionTypeId1");

                    b.Property<string>("Task");

                    b.Property<string>("Tip");

                    b.HasKey("Id");

                    b.HasIndex("QuestionTypeId");

                    b.HasIndex("QuestionTypeId1");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Cranium.Data.Models.QuestionType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid?>("CategoryId1");

                    b.Property<string>("Explanation");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CategoryId1");

                    b.ToTable("QuestionTypes");
                });

            modelBuilder.Entity("Cranium.Data.Models.Answer", b =>
                {
                    b.HasOne("Cranium.Data.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cranium.Data.Models.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId1");
                });

            modelBuilder.Entity("Cranium.Data.Models.Question", b =>
                {
                    b.HasOne("Cranium.Data.Models.QuestionType", "QuestionType")
                        .WithMany()
                        .HasForeignKey("QuestionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cranium.Data.Models.QuestionType")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionTypeId1");
                });

            modelBuilder.Entity("Cranium.Data.Models.QuestionType", b =>
                {
                    b.HasOne("Cranium.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cranium.Data.Models.Category")
                        .WithMany("QuestionTypes")
                        .HasForeignKey("CategoryId1");
                });
#pragma warning restore 612, 618
        }
    }
}
