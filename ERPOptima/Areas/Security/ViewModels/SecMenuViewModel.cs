using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ERPOptima.Web.Security.ViewModels
{
    public class SecMenuViewModel
    {
        public SecMenuViewModel()
        {
            items = new List<SecMenuViewModel>();
        }
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        public Nullable<int> SecResourceId { get; set; }
        public List<SecMenuViewModel> items { get; set; }
    }
}