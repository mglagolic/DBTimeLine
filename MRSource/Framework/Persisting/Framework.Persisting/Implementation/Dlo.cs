﻿using System.Collections.Generic;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting.Implementation
{
    public class Dlo : IDlo
    {
        private Dictionary<string, object> _ColumnValues = new Dictionary<string, object>();
        public Dictionary<string, object> ColumnValues
        {
            get
            {
                return _ColumnValues;
            }
        }

    }
}
