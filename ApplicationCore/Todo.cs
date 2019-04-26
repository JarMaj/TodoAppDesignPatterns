using System;
using System.Collections;
using System.Collections.Generic;

namespace ApplicationCore
{
    public class Todo
    {
        public Guid TodoID { get; set; }
        public string TodoName { get; set; }
        public Guid UserID { get; set; }

        public List<QTask> QTasks { get; set; }
        public User User { get; set; }
        
    }
}
