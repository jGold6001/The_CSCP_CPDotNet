using DataLayer;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace _03___Model
{
    public class EFClientOp : EFAdapter, IClientOperations
    {
        public EFClientOp() 
        {
           
        }

        public List<Place> GetPlaces
        {
            get
            {
                db.Places.Load();
                db.Cars.Load();
                db.Tariffs.Load();
                db.Clients.Load();
                db.RentValues.Load();
                return db.Places.ToList();
            }
        }       

        public List<RentValue> RentTypes
        {
            get
            {
                return db.RentValues.ToList();
            }
        }

        public List<Record> RecordList
        {
            get
            {
                return db.Places.Select(p => new Record()
                {
                    NumberPLace = p.Number,
                    ClientLastName = p.Client.LastName,
                    CarBrand = p.Car.Brand,
                    DatePayment = p.Tariff.DatePayment,
                    Rent = p.Tariff.RentValue.Price,
                    Debt = p.Tariff.Debt
                }).ToList();
            }
        }

        public override void Add(object newObj)
        {
            //номер парковки не может повторятся в базе
            Place place = newObj as Place;
            foreach (Place placeCurr in GetPlaces)
            {
                if (place.Number == placeCurr.Number)
                    return;
            }
            db.Places.Add(place);
            try
            {
                db.SaveChanges();
            }
            catch
            {
                //если не все данные будут заполнены
                return;
            }
        }

        public override void Edit(object newObj)
        {
            Place newPlace = newObj as Place;
            foreach (Place placeCurrent in db.Places.Local)
            {
                if (placeCurrent.Number == newPlace.Number)
                {
                    //write not changed data
                    newPlace.Client.DateRegistred = placeCurrent.Client.DateRegistred;
                    newPlace.Tariff.DatePayment = placeCurrent.Tariff.DatePayment;
                    newPlace.Tariff.Deposit = placeCurrent.Tariff.Deposit;
                    newPlace.Tariff.Debt = placeCurrent.Tariff.Debt;

                    //delete old data
                    db.Clients.Remove(placeCurrent.Client);
                    db.Cars.Remove(placeCurrent.Car);
                    db.Tariffs.Remove(placeCurrent.Tariff);
                    db.Places.Remove(placeCurrent);

                    db.Places.Add(newPlace);
                    break;
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch
            {
                //если не все данные будут заполнены
                return;
            }     
        }

        public override void Remove(object curObj)
        {
            Record record = curObj as Record;
            foreach (Place place in db.Places)
            {
                if (record.NumberPLace == place.Number)
                {                    
                    db.Cars.Remove(place.Car);
                    db.Clients.Remove(place.Client);
                    db.Tariffs.Remove(place.Tariff);
                    db.Places.Remove(place);
                    break;
                }
            }
            db.SaveChanges();
        }

        public void UpdateTariff()
        {
            foreach (var item in db.Tariffs)
            {
                if (DateTime.Now > item.DatePayment)
                {
                    item.Debt = Calculator.Debt(item);
                    item.DatePayment = Calculator.DatePayment(item);
                }
            }

        }

    }
}