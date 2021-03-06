﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StackUnderFlow.Data;

namespace StackUnderFlow.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181109061035_RemoveTopics")]
    partial class RemoveTopics
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StackUnderFlow.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired();

                    b.Property<string>("AuthorId")
                        .IsRequired();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DownVotes");

                    b.Property<int>("Inappropriate");

                    b.Property<int>("ResponseId");

                    b.Property<int>("UpVotes");

                    b.HasKey("Id");

                    b.HasIndex("ResponseId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("StackUnderFlow.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Answered");

                    b.Property<string>("Author");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DownVotes");

                    b.Property<int>("Inappropriate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UpVotes");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("StackUnderFlow.Entities.QuestionTopics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("QuestionId");

                    b.Property<int>("TopicId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TopicId");

                    b.ToTable("QuestionTopics");
                });

            modelBuilder.Entity("StackUnderFlow.Entities.Response", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DownVotes");

                    b.Property<int>("Inappropriate");

                    b.Property<int>("QuestionId");

                    b.Property<bool>("Solution");

                    b.Property<int>("UpVotes");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Responses");
                });

            modelBuilder.Entity("StackUnderFlow.Entities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("QuestionId");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StackUnderFlow.Entities.Comment", b =>
                {
                    b.HasOne("StackUnderFlow.Entities.Response", "Response")
                        .WithMany()
                        .HasForeignKey("ResponseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StackUnderFlow.Entities.QuestionTopics", b =>
                {
                    b.HasOne("StackUnderFlow.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StackUnderFlow.Entities.Topic", "Topic")
                        .WithMany("Questions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StackUnderFlow.Entities.Response", b =>
                {
                    b.HasOne("StackUnderFlow.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
