using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Domain.Content
{
    [Table("Slides")]
    public class Slide
    {
        [Key]
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }
        public string Image { get; set; }
        public int SortOrder { get; set; }
        public PostStatus Status { set; get; }
    }
}
