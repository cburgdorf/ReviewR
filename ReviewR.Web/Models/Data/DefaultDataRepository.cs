﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ReviewR.Web.Infrastructure;

namespace ReviewR.Web.Models.Data
{
    public interface IDataRepository
    {
        IEntitySet<Role> Roles { get; }
        IEntitySet<User> Users { get; }
        IEntitySet<Review> Reviews { get; }
        IEntitySet<FileChange> Changes { get; }
        IEntitySet<Comment> Comments { get; }
        IEntitySet<Iteration> Iterations { get; }
        IEntitySet<Credential> Credentials { get; }
        int SaveChanges();
    }

    public class DefaultDataRepository : IDataRepository
    {
        private ReviewRDbContext _db;

        public virtual IEntitySet<Role> Roles { get; private set; }
        public virtual IEntitySet<User> Users { get; private set; }
        public virtual IEntitySet<Review> Reviews { get; private set; }
        public virtual IEntitySet<FileChange> Changes { get; private set; }
        public virtual IEntitySet<Comment> Comments { get; private set; }
        public virtual IEntitySet<Iteration> Iterations { get; private set; }
        public virtual IEntitySet<Credential> Credentials { get; private set; }

        public DefaultDataRepository()
            : base()
        {
            _db = new ReviewRDbContext();
            Users = new DbSetAdaptor<User>(_db.Users);
            Roles = new DbSetAdaptor<Role>(_db.Roles);
            Reviews = new DbSetAdaptor<Review>(_db.Reviews);
            Changes = new DbSetAdaptor<FileChange>(_db.Changes);
            Comments = new DbSetAdaptor<Comment>(_db.Comments);
            Iterations = new DbSetAdaptor<Iteration>(_db.Iterations);
            Credentials = new DbSetAdaptor<Credential>(_db.Credentials);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }

    public class ReviewRDbContext : DbContext
    {
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<FileChange> Changes  { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Iteration> Iterations { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany<Role>(u => u.Roles)
                        .WithMany(r => r.Users)
                        .Map(m => m.ToTable("UserRoles"));
        }
    }
}