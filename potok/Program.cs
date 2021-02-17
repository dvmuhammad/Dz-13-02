﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace potok
{

    class Client
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        
        public void Insert(int id,decimal zedball)
        {
            this.Id = id;
            this.Balance = zedball;
            Program.clients.Add(new Client(){Id = id,Balance = zedball});
            Program.balances.Add(new Client(){Id = id,Balance = zedball});
            Console.WriteLine("Successfully added client with Id " + id);
            
        }
        
        public void Update(int id,decimal zedball)
        {
            
            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (id == Program.clients[i].Id)
                {
                    Program.clients[i].Balance = zedball;
                    
                }
            }
        }
        public void Select(int id)
        {
            for (int i = 0; i < Program.clients.Count; i++)
            {
                if (id == Program.clients[i].Id)
                {
                    Console.WriteLine("ID: "+Program.clients[i].Id);
                    Console.WriteLine("Balance: "+Program.clients[i].Balance);
                    
                }
            }
        }
        public void Delete(int id)
        {
            for(int i =0; i < Program.clients.Count;i++)
            {
              if(id == Program.clients[i].Id)
                {
                    Program.clients.RemoveAt(i);
                    
                }  
            } 
        }
    }

    class Program
    {
        public static List<Client> clients = new List<Client>();
        public static List<Client> balances = new List<Client>();
        static void Main(string[] args)
        {
            balances.AddRange(clients);
            Client client = new Client();
            string choice = "1";
            while (choice != "5")
            {
                Console.Clear();
                TimerCallback kull = new TimerCallback(Clientsbl);
                Timer x = new Timer(kull, clients, 0, 1000);
                Console.WriteLine("1.Add a client\n2.Change the customer's balance\n3.Delete a client\n4.Show the customer's balance");
                choice = Console.ReadLine();
                int id = 0;
                switch (choice)
                {
                    case "1":
                        Console.Write("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.Write("balance: ");
                        decimal zedball = decimal.Parse(Console.ReadLine());
                        Thread novainsert = new Thread(new ThreadStart(() => { client.Insert(id, zedball); }));
                        novainsert.Start();
                        break;
                    case "2":
                        Console.WriteLine("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("new balance: ");
                        decimal balancec = decimal.Parse(Console.ReadLine());
                        Thread novauptade = new Thread(new ThreadStart(() => { client.Update(id, balancec); }));
                        novauptade.Start();
                        break;
                    case "3":
                        Console.Write("Id: ");
                        id = int.Parse(Console.ReadLine());
                        Console.WriteLine("The customer removed");
                        Thread novadelete = new Thread(new ThreadStart(() => { client.Delete(id); }));
                        novadelete.Start();
                        break;
                    case "4":
                        Console.WriteLine("Id: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Thread novaselect = new Thread(new ThreadStart(() => { client.Select(id); }));
                        novaselect.Start();
                        break;
                }
                Console.ReadKey();
            }
        }
        static void Clientsbl(object obj)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Balance != balances[i].Balance)
                {
                    Console.ForegroundColor = (balances[i].Balance <= clients[i].Balance)?ConsoleColor.Green:ConsoleColor.Red;
                    string Difference = (balances[i].Balance <= clients[i].Balance)?$"+{clients[i].Balance-balances[i].Balance}":$"{clients[i].Balance-balances[i].Balance}";
                    Console.WriteLine($"Id client: {clients[i].Id} -- Old balance: {balances[i].Balance} -- Changed balance: {clients[i].Balance} -- Difference: "+ Difference);
                    balances[i].Balance = clients[i].Balance;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
        }
    }
}

//P.S не оч,хорошо шарю в инглиш :)
