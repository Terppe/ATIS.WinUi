﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATIS.WinUi.DataLayer.Models
{
    public class Tbl72PlSpecies
    {
        [Key]
        public int PlSpeciesId { get; set; }
        public string PlSpeciesName { get; set; }
        public string Subspecies { get; set; }
        public string Divers { get; set; }
        public int GenusId { get; set; }
        public int? SpeciesgroupId { get; set; }
        public int CountId { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string MemoSpecies { get; set; }
        public string TradeName { get; set; }
        public string Author { get; set; }
        public string AuthorYear { get; set; }
        public string Importer { get; set; }
        public string ImportingYear { get; set; }
        public int? BasinHeight { get; set; }
        public decimal? PlantLength { get; set; }
        public bool? Difficult1 { get; set; }
        public bool? Difficult2 { get; set; }
        public bool? Difficult3 { get; set; }
        public bool? Difficult4 { get; set; }
        public string MemoTech { get; set; }
        public decimal? Ph1 { get; set; }
        public decimal? Ph2 { get; set; }
        public int? Temp1 { get; set; }
        public int? Temp2 { get; set; }
        public int? Hardness1 { get; set; }
        public int? Hardness2 { get; set; }
        public int? CarboHardness1 { get; set; }
        public int? CarboHardness2 { get; set; }
        public string Writer { get; set; }
        public DateTime WriterDate { get; set; }
        public string Updater { get; set; }
        public DateTime UpdaterDate { get; set; }
        public string MemoBuilt { get; set; }
        public string MemoColor { get; set; }
        public string MemoReproduction { get; set; }
        public string MemoCulture { get; set; }
        public string MemoGlobal { get; set; }
        //   public byte[] RowVersion { get; set; }

        [ForeignKey("GenusId")]
        public virtual Tbl66Genus Tbl66Genusses { get; set; }
        [ForeignKey("SpeciesgroupId")]
        public virtual Tbl68Speciesgroup Tbl68Speciesgroups { get; set; }
        public virtual ICollection<Tbl78Name> Tbl78Names { get; set; }
        public virtual ICollection<Tbl81Image> Tbl81Images { get; set; }
        public virtual ICollection<Tbl84Synonym> Tbl84Synonyms { get; set; }
        public virtual ICollection<Tbl87Geographic> Tbl87Geographics { get; set; }
        public virtual ICollection<Tbl90Reference> Tbl90References { get; set; }
        public virtual ICollection<Tbl93Comment> Tbl93Comments { get; set; }


    }
}
