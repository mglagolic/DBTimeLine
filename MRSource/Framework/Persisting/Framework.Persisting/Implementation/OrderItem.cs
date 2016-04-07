using System;
using Framework.Persisting.Enums;
using Framework.Persisting.Interfaces;

namespace Framework.Persisting.Implementation
{
    public class OrderItem : IOrderItem
    {
        private eOrderDirection _Direction = eOrderDirection.Ascending;
        public eOrderDirection Direction
        {
            get
            {
                return _Direction;
            }

            set
            {
                _Direction = value;
            }
        }

        public string Name { get; set; }

    }
}
