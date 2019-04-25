using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublisherScheduler01Web.DataObjects
{
    public static class Constants
    {
        public enum SecurityLevel
        {
            Administrator,
            Owner,
            Manager,
            Overseer,
            Captain,
            User
        } 


    }
}