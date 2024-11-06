using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities;
public class Admin
{
    public int Id { get; set; }
    public string Username { get; set; }

    public string Password { get; set; }
}
