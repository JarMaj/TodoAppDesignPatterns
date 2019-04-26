using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class TodoViewModel : Todo
    {

        public Guid TodoID { get; set; }
        public string TodoName { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }

        public List<QTaskViewModel> QTasks { get; set; }
        public UserViewModel User { get; set; }
    }
}
