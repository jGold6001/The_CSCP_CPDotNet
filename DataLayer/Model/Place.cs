﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Place
    {
        [Key]
        public int Id { get; set; }        
        public int Number { get; set; }      
        public int? ClientId { get; set; }       
        public int? CarId { get; set; }       
        public int? TarriffId { get; set; }
        public Client Client { get; set; }
        public Car Car { get; set; }
        public Tarriff Tarriff{get;set;}

        public override string ToString()
        {
            if(Client != null && Car !=null && Tarriff != null)
            {
                return String.Format("Place #{0}\nClient: {1} {5}\ncar: {2} {6}\ndate register: {3}\ntarriff: {4}\n\n", Number, Client.LastName, Car.Brand, Client.DateRegistred.ToShortDateString(), Tarriff.Cost, Client.FirstName, Car.Color);
            }
            return String.Format("Place #{0} - Empty", Number);
        }
    }
}