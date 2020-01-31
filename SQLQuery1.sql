/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2016                     */
/* Date de création :  07/03/2018 18:21:20                      */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Consommation') and o.name = 'FK_CONSOMMA_CONSOMME_LIGNE')
alter table Consommation
   drop constraint FK_CONSOMMA_CONSOMME_LIGNE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Consommation') and o.name = 'FK_CONSOMMA_CONSOMME_SERVICE')
alter table Consommation
   drop constraint FK_CONSOMMA_CONSOMME_SERVICE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Facture') and o.name = 'FK_FACTURE_FACTURER_CONSOMMA')
alter table Facture
   drop constraint FK_FACTURE_FACTURER_CONSOMMA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Internet') and o.name = 'FK_INTERNET_GENERALIS_SERVICE')
alter table Internet
   drop constraint FK_INTERNET_GENERALIS_SERVICE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Ligne') and o.name = 'FK_LIGNE_POSSEDER_ABONNE')
alter table Ligne
   drop constraint FK_LIGNE_POSSEDER_ABONNE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Mobile') and o.name = 'FK_MOBILE_GENERALIS_LIGNE')
alter table Mobile
   drop constraint FK_MOBILE_GENERALIS_LIGNE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Telephonique') and o.name = 'FK_TELEPHON_GENERALIS_LIGNE')
alter table Telephonique
   drop constraint FK_TELEPHON_GENERALIS_LIGNE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Vocale') and o.name = 'FK_VOCALE_GENERALIS_SERVICE')
alter table Vocale
   drop constraint FK_VOCALE_GENERALIS_SERVICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Abonne')
            and   type = 'U')
   drop table Abonne
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Consommation')
            and   name  = 'CONSOMME_FK2'
            and   indid > 0
            and   indid < 255)
   drop index Consommation.CONSOMME_FK2
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Consommation')
            and   name  = 'CONSOMME_FK'
            and   indid > 0
            and   indid < 255)
   drop index Consommation.CONSOMME_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Consommation')
            and   type = 'U')
   drop table Consommation
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Facture')
            and   name  = 'FACTURER_FK'
            and   indid > 0
            and   indid < 255)
   drop index Facture.FACTURER_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Facture')
            and   type = 'U')
   drop table Facture
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Internet')
            and   type = 'U')
   drop table Internet
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Ligne')
            and   name  = 'POSSEDER_FK'
            and   indid > 0
            and   indid < 255)
   drop index Ligne.POSSEDER_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Ligne')
            and   type = 'U')
   drop table Ligne
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Mobile')
            and   type = 'U')
   drop table Mobile
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Service')
            and   type = 'U')
   drop table Service
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Telephonique')
            and   type = 'U')
   drop table Telephonique
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Vocale')
            and   type = 'U')
   drop table Vocale
go

if exists(select 1 from systypes where name='Isp')
   execute sp_unbindrule Isp
go

if exists(select 1 from systypes where name='Isp')
   drop type Isp
go

if exists(select 1 from systypes where name='Operateur')
   execute sp_unbindrule Operateur
go

if exists(select 1 from systypes where name='Operateur')
   drop type Operateur
go

if exists (select 1 from sysobjects where id=object_id('R_Isp') and type='R')
   drop rule  R_Isp
go

if exists (select 1 from sysobjects where id=object_id('R_Operateur') and type='R')
   drop rule  R_Operateur
go

create rule R_Isp as
      @column in ('oredoo','telecom','orange','globalNet','topnet','hexaByte')
go

create rule R_Operateur as
      @column in ('orange','telecom','oredoo')
go

/*==============================================================*/
/* Domaine : Isp                                                */
/*==============================================================*/
create type Isp
   from char(9)
go

execute sp_bindrule R_Isp, Isp
go

/*==============================================================*/
/* Domaine : Operateur                                          */
/*==============================================================*/
create type Operateur
   from char(7)
go

execute sp_bindrule R_Operateur, Operateur
go

/*==============================================================*/
/* Table : Abonne                                               */
/*==============================================================*/
create table Abonne (
   numS                 int                  not null,
   prenom               varchar(254)         null,
   nom                  varchar(254)         null,
   adresse              varchar(254)         null,
   constraint PK_ABONNE primary key (numS)
)
go

/*==============================================================*/
/* Table : Consommation                                         */
/*==============================================================*/
create table Consommation (
   numL                 int                  not null,
   numS                 int                  not null,
   id                   int                  not null,
   mois                 int                  null,
   annee                int                  null,
   nbUniteInternet      int                  null,
   nbUniteVocale        int                  null,
   constraint PK_CONSOMMATION primary key (numL, numS, id)
)
go

/*==============================================================*/
/* Index : CONSOMME_FK                                          */
/*==============================================================*/




create nonclustered index CONSOMME_FK on Consommation (numL ASC)
go

/*==============================================================*/
/* Index : CONSOMME_FK2                                         */
/*==============================================================*/




create nonclustered index CONSOMME_FK2 on Consommation (numS ASC)
go

/*==============================================================*/
/* Table : Facture                                              */
/*==============================================================*/
create table Facture (
   numL                 int                  not null,
   numS                 int                  not null,
   id                   int                  not null,
   montant              float                null
)
go

/*==============================================================*/
/* Index : FACTURER_FK                                          */
/*==============================================================*/




create nonclustered index FACTURER_FK on Facture (numL ASC,
  numS ASC,
  id ASC)
go

/*==============================================================*/
/* Table : Internet                                             */
/*==============================================================*/
create table Internet (
   numS                 int                  not null,
   isp                  ISP                  null,
   constraint PK_INTERNET primary key (numS)
)
go

/*==============================================================*/
/* Table : Ligne                                                */
/*==============================================================*/
create table Ligne (
   numL                 int                  not null,
   numS                 int                  not null,
   constraint PK_LIGNE primary key (numL)
)
go

/*==============================================================*/
/* Index : POSSEDER_FK                                          */
/*==============================================================*/




create nonclustered index POSSEDER_FK on Ligne (numS ASC)
go

/*==============================================================*/
/* Table : Mobile                                               */
/*==============================================================*/
create table Mobile (
   numL                 int                  not null,
   reseau               varchar(254)         null,
   constraint PK_MOBILE primary key (numL)
)
go

/*==============================================================*/
/* Table : Service                                              */
/*==============================================================*/
create table Service (
   numS                 int                  not null,
   descS                varchar(254)         null,
   prixS                float                null,
   constraint PK_SERVICE primary key (numS)
)
go

/*==============================================================*/
/* Table : Telephonique                                         */
/*==============================================================*/
create table Telephonique (
   numL                 int                  not null,
   locT                 varchar(254)         null,
   constraint PK_TELEPHONIQUE primary key (numL)
)
go

/*==============================================================*/
/* Table : Vocale                                               */
/*==============================================================*/
create table Vocale (
   numS                 int                  not null,
   operateur            Operateur            null,
   constraint PK_VOCALE primary key (numS)
)
go

alter table Consommation
   add constraint FK_CONSOMMA_CONSOMME_LIGNE foreign key (numL)
      references Ligne (numL)
go

alter table Consommation
   add constraint FK_CONSOMMA_CONSOMME_SERVICE foreign key (numS)
      references Service (numS)
go

alter table Facture
   add constraint FK_FACTURE_FACTURER_CONSOMMA foreign key (numL, numS, id)
      references Consommation (numL, numS, id)
go

alter table Internet
   add constraint FK_INTERNET_GENERALIS_SERVICE foreign key (numS)
      references Service (numS)
go

alter table Ligne
   add constraint FK_LIGNE_POSSEDER_ABONNE foreign key (numS)
      references Abonne (numS)
go

alter table Mobile
   add constraint FK_MOBILE_GENERALIS_LIGNE foreign key (numL)
      references Ligne (numL)
go

alter table Telephonique
   add constraint FK_TELEPHON_GENERALIS_LIGNE foreign key (numL)
      references Ligne (numL)
go

alter table Vocale
   add constraint FK_VOCALE_GENERALIS_SERVICE foreign key (numS)
      references Service (numS)
go

