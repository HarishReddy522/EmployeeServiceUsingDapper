using EmployeeModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeBusinessLayer
{
    public interface IOrdersItemsRepository
    {
        List<Order> GetAllOrders();

        
    }
}
