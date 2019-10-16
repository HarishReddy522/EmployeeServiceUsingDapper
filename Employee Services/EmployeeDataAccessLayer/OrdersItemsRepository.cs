using EmployeeBusinessLayer;
using EmployeeModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace EmployeeDataAccessLayer
{
    public class OrdersItemsRepository : IOrdersItemsRepository
    {

        IOptions<ReadConfig> _Connectionstring;
        public OrdersItemsRepository(IOptions<ReadConfig> Connectionstring)
        {
            _Connectionstring = Connectionstring;
        }
        public List<Order> GetAllOrders()
        {
            using (var con = new SqlConnection(_Connectionstring.Value.Connectionstring))
            {
                string sql = "select * from [Order] O LEFT JOIN Customer C on O.CId=C.CId LEFT JOIN  ItemsOrders IOs on O.OrId=IOs.OrId INNER JOIN  Items I on I.ItemId=IOS.ItemId order by O.OrId";
                var orderDic = new Dictionary<int, Order>();

                var orderlist = con.Query<Order, Customer, Item, Order>(sql,
                    map: (O, C, I) =>
                    {
                        Order Orderentry;
                        if (!orderDic.TryGetValue(O.OrId, out Orderentry))
                        {
                            Orderentry = O;
                            Orderentry.customer = new Customer();
                            // Orderentry.customer = C;
                            Orderentry.items = new List<Item>();
                        }
                        Orderentry.items.Add(I);
                        Orderentry.customer = C;
                        return Orderentry;
                    },
                    splitOn: "CId,OrId"
                    ).Distinct().ToList();

                var orderlist_Res = orderlist.Select(o => new Order
                {
                    OrId = o.OrId,
                    OrderDate = o.OrderDate,
                    Amount = o.Amount,
                    customer = o.customer,
                    items = orderlist.Where(x => x.OrId == o.OrId).SelectMany(t => t.items).ToList()
                }).Distinct().GroupBy(a => a.OrId).Select(Or => Or.FirstOrDefault()).ToList();

                //})

                return orderlist_Res;
            }
        }
    }
      
}
