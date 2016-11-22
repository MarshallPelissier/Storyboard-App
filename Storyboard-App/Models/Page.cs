using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Storyboard_App.Models
{
    public class Page
    {
        public int Id { get; set; }

        [Required]
        public int Num { get; set; }

        public string RoughSketch { get; set; }
        public string NotesSketch { get; set; }
        public string FinalSketch { get; set; }
        public string Description { get; set; }
        public string Script { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}