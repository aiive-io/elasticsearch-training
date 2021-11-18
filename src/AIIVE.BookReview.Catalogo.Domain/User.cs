using AIIVE.BookReview.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIIVE.BookReview.Catalogo.Domain
{
    public class User : Entity<long>
    {
        public string Login { get; private set; }
    }
}
