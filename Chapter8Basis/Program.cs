using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrudImplementations;
using Model;

namespace Chapter8Basis
{
    class Program
    {
        static void Main(string[] args)
        {
            Order ord = new Order();
            ord.product = "candy canes";
            ord.amount = 100;

            Item itm = new Item();
            itm.product = "candy cane";
            itm.cost = 1.99;

            Console.WriteLine("=========CreateSeparateServices=========");
            OrderController sep = CreateSeparateServices();
            sep.CreateOrder(ord);

            Console.WriteLine("=========CreateSingleService=========");
            OrderController sing = CreateSingleService();
            sing.CreateOrder(ord);

            Console.WriteLine("=========CreateSingleItemService=========");
            ItemController singItem = CreateSingleItemService();
            singItem.CreateOrder(itm);

            Console.WriteLine("=========GenericController<Order>=========");
            GenericController<Order> generic = CreateGenericServices();
            generic.CreateEntity(ord);

            Console.WriteLine("=========GenericController<Item>=========");
            GenericController<Item> genericItem = CreateGenericItemServices();
            genericItem.CreateEntity(itm);


            Console.WriteLine("Hit any key to quit");
            Console.ReadKey();
        }

        static OrderController CreateSeparateServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            return new OrderController(reader, saver, deleter);
        }

        static OrderController CreateSingleService()
        {
            var crud = new Crud<Order>();
            return new OrderController(crud, crud, crud);
        }

        static ItemController CreateSingleItemService()
        {
            var crud = new Crud<Item>();
            return new ItemController(crud, crud, crud);
        }

        static GenericController<Order> CreateGenericServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            // This must be declared using reflection...
            GenericController<Order> ctl = (GenericController<Order>)Activator.CreateInstance(typeof(GenericController<Order>), reader, saver, deleter);
            //This does not work 
            //GenericController<Order> ctl = new GenericController(reader, saver, deleter);
            return ctl;
        }

        static GenericController<Item> CreateGenericItemServices()
        {
            var reader = new Reader<Item>();
            var saver = new Saver<Item>();
            var deleter = new Deleter<Item>();
            // This must be declared using reflection...
            GenericController<Item> ctl = (GenericController<Item>)Activator.CreateInstance(typeof(GenericController<Item>), reader, saver, deleter);
            //This does not work 
            //GenericController<Order> ctl = new GenericController(reader, saver, deleter);
            return ctl;
        }

    }
}
