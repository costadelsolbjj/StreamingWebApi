using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestModule1.Models
{
    public class Songs
    {
        [Key]
        public int SongId { get; set; }

        [Required(ErrorMessage = "Song Name Required")]
        [StringLength(maximumLength:100,ErrorMessage = "Song Name must not exceed 100 characters")]
        public string SongName { get; set; }

        [Required(ErrorMessage = "Song File Cover Required")]
        [StringLength(maximumLength: 500, ErrorMessage = "Song File cover must not exceed 500 characters")]
        public string SongFileCover { get; set; }

        [Required(ErrorMessage = "Song URL Required")]
        [StringLength(maximumLength: 800, ErrorMessage = "Song URL cover must not exceed 800 characters")]
        public string SongUrl { get; set; }

        [Required(ErrorMessage = "Song Duration Required")]
        [StringLength(maximumLength: 10, ErrorMessage = "Song Duration cover must not exceed 10 characters")]
        public string SongDuration { get; set; }

        [Required(ErrorMessage = "Singer Name Required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Singer Name must not exceed 100 characters")]
        public string SingerName { get; set; }


    }
}