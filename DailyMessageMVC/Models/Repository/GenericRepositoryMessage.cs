using DailyMessageMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyMessageMVC.Models.Repository
{
    public class GenericRepositoryMessage<T> : DBContextMessage<T>, IUnitOfWork<T> where T : class
    {
        
    }
}