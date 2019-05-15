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
            Administrator = 0,
            Owner = 1,
            Manager = 2,
            Overseer = 3,
            Captain = 4,
            User = 5
        }

        public enum Congregations
        {
            Indonesian = 922740,
            Ellsworth = 73510,
            Italian = 119099,
            Penrose = 73791
        } 


    }
}