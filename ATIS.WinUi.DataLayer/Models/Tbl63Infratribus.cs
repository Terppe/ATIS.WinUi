﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ATIS.WinUi.DataLayer.Models
{
    public class Tbl63Infratribus
    {
        [Key]

        public int InfratribusId { get; set; }
        public string InfratribusName { get; set; }
        public int SubtribusId { get; set; }
        public int CountId { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string Synonym { get; set; }
        public string Author { get; set; }
        public string AuthorYear { get; set; }
        public string Info { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }
        public string Writer { get; set; }
        public DateTime WriterDate { get; set; }
        public string Updater { get; set; }
        public DateTime UpdaterDate { get; set; }
        public string Memo { get; set; }
        //    public byte[] RowVersion { get; set; }

    }
}
