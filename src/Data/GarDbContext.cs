﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using blog_api.Models;

namespace blog_api.Data;

public partial class GarDbContext : DbContext
{
    public GarDbContext()
    {
    }

    public GarDbContext(DbContextOptions<GarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AsAddrObj> AsAddrObjs { get; set; }

    public virtual DbSet<AsAdmHierarchy> AsAdmHierarchies { get; set; }

    public virtual DbSet<AsHouse> AsHouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AsAddrObj>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Addr_Objs");

            entity.ToTable("as_addr_obj", "fias", tb => tb.HasComment("Сведения классификатора адресообразующих элементов"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле")
                .HasColumnName("id");
            entity.Property(e => e.Changeid)
                .HasComment("ID изменившей транзакции")
                .HasColumnName("changeid");
            entity.Property(e => e.Enddate)
                .HasComment("Окончание действия записи")
                .HasColumnName("enddate");
            entity.Property(e => e.Isactive)
                .HasComment("Признак действующего адресного объекта")
                .HasColumnName("isactive");
            entity.Property(e => e.Isactual)
                .HasComment("Статус актуальности адресного объекта ФИАС")
                .HasColumnName("isactual");
            entity.Property(e => e.Level)
                .HasComment("Уровень адресного объекта")
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .HasComment("Наименование")
                .HasColumnName("name");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью")
                .HasColumnName("nextid");
            entity.Property(e => e.Objectguid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID")
                .HasColumnName("objectguid");
            entity.Property(e => e.Objectid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа INTEGER")
                .HasColumnName("objectid");
            entity.Property(e => e.Opertypeid)
                .HasComment("Статус действия над записью – причина появления записи")
                .HasColumnName("opertypeid");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью")
                .HasColumnName("previd");
            entity.Property(e => e.Startdate)
                .HasComment("Начало действия записи")
                .HasColumnName("startdate");
            entity.Property(e => e.Typename)
                .HasComment("Краткое наименование типа объекта")
                .HasColumnName("typename");
            entity.Property(e => e.Updatedate)
                .HasComment("Дата внесения (обновления) записи")
                .HasColumnName("updatedate");
        });

        modelBuilder.Entity<AsAdmHierarchy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Adm_Hier");

            entity.ToTable("as_adm_hierarchy", "fias", tb => tb.HasComment("Сведения по иерархии в административном делении"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле")
                .HasColumnName("id");
            entity.Property(e => e.Areacode)
                .HasComment("Код района")
                .HasColumnName("areacode");
            entity.Property(e => e.Changeid)
                .HasComment("ID изменившей транзакции")
                .HasColumnName("changeid");
            entity.Property(e => e.Citycode)
                .HasComment("Код города")
                .HasColumnName("citycode");
            entity.Property(e => e.Enddate)
                .HasComment("Окончание действия записи")
                .HasColumnName("enddate");
            entity.Property(e => e.Isactive)
                .HasComment("Признак действующего адресного объекта")
                .HasColumnName("isactive");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью")
                .HasColumnName("nextid");
            entity.Property(e => e.Objectid)
                .HasComment("Глобальный уникальный идентификатор объекта")
                .HasColumnName("objectid");
            entity.Property(e => e.Parentobjid)
                .HasComment("Идентификатор родительского объекта")
                .HasColumnName("parentobjid");
            entity.Property(e => e.Path)
                .HasComment("Материализованный путь к объекту (полная иерархия)")
                .HasColumnName("path");
            entity.Property(e => e.Placecode)
                .HasComment("Код населенного пункта")
                .HasColumnName("placecode");
            entity.Property(e => e.Plancode)
                .HasComment("Код ЭПС")
                .HasColumnName("plancode");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью")
                .HasColumnName("previd");
            entity.Property(e => e.Regioncode)
                .HasComment("Код региона")
                .HasColumnName("regioncode");
            entity.Property(e => e.Startdate)
                .HasComment("Начало действия записи")
                .HasColumnName("startdate");
            entity.Property(e => e.Streetcode)
                .HasComment("Код улицы")
                .HasColumnName("streetcode");
            entity.Property(e => e.Updatedate)
                .HasComment("Дата внесения (обновления) записи")
                .HasColumnName("updatedate");
        });

        modelBuilder.Entity<AsHouse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Houses");

            entity.ToTable("as_houses", "fias", tb => tb.HasComment("Сведения по номерам домов улиц городов и населенных пунктов"));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Уникальный идентификатор записи. Ключевое поле")
                .HasColumnName("id");
            entity.Property(e => e.Addnum1)
                .HasComment("Дополнительный номер дома 1")
                .HasColumnName("addnum1");
            entity.Property(e => e.Addnum2)
                .HasComment("Дополнительный номер дома 1")
                .HasColumnName("addnum2");
            entity.Property(e => e.Addtype1)
                .HasComment("Дополнительный тип дома 1")
                .HasColumnName("addtype1");
            entity.Property(e => e.Addtype2)
                .HasComment("Дополнительный тип дома 2")
                .HasColumnName("addtype2");
            entity.Property(e => e.Changeid)
                .HasComment("ID изменившей транзакции")
                .HasColumnName("changeid");
            entity.Property(e => e.Enddate)
                .HasComment("Окончание действия записи")
                .HasColumnName("enddate");
            entity.Property(e => e.Housenum)
                .HasComment("Основной номер дома")
                .HasColumnName("housenum");
            entity.Property(e => e.Housetype)
                .HasComment("Основной тип дома")
                .HasColumnName("housetype");
            entity.Property(e => e.Isactive)
                .HasComment("Признак действующего адресного объекта")
                .HasColumnName("isactive");
            entity.Property(e => e.Isactual)
                .HasComment("Статус актуальности адресного объекта ФИАС")
                .HasColumnName("isactual");
            entity.Property(e => e.Nextid)
                .HasComment("Идентификатор записи связывания с последующей исторической записью")
                .HasColumnName("nextid");
            entity.Property(e => e.Objectguid)
                .HasComment("Глобальный уникальный идентификатор адресного объекта типа UUID")
                .HasColumnName("objectguid");
            entity.Property(e => e.Objectid)
                .HasComment("Глобальный уникальный идентификатор объекта типа INTEGER")
                .HasColumnName("objectid");
            entity.Property(e => e.Opertypeid)
                .HasComment("Статус действия над записью – причина появления записи")
                .HasColumnName("opertypeid");
            entity.Property(e => e.Previd)
                .HasComment("Идентификатор записи связывания с предыдущей исторической записью")
                .HasColumnName("previd");
            entity.Property(e => e.Startdate)
                .HasComment("Начало действия записи")
                .HasColumnName("startdate");
            entity.Property(e => e.Updatedate)
                .HasComment("Дата внесения (обновления) записи")
                .HasColumnName("updatedate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
